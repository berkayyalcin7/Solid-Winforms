using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Message;
using SolidOtomasyon.Takip.Model.Entities.Base;
using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;
using SolidOtomasyon.UserControls.Controls;
using SolidOtomasyon.UserControls.Grid;
using System;
using System.Windows.Forms;

namespace SolidOtomasyon.Forms.BaseForms
{
    public partial class BaseEditForm : RibbonForm
    {
        private bool _formSablonKayitEdilecek;

        //Protected : kalıtım alınan sınıflar kullanabilir.
        protected internal IslemTuru IslemTuru;

        //id Alanlarımızı BAse'de yapacağız
        protected internal long Id;

        protected internal bool RefreshYapilacak;

        protected MyDataLayoutControl DataLayoutControl;
        protected MyDataLayoutControl[] DataLayoutControls;

        protected IBaseBll Bll;

        protected KartTuru BaseEditKartTuru;

        //Old Entity ve Current Entity oluşturacağız Update İçin
        protected BaseEntity OldEntity;

        protected BaseEntity CurrentEntity;

        protected bool IsLoaded;

        protected bool KayitSonrasiFormuKapat = true;

        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;


        public BaseEditForm()
        {
            InitializeComponent();
        }


        protected void EventsLoad()
        {
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;


            //Form Events
            Load += BaseEditForm_Load;
            FormClosing += BaseEditForm_FormClosing;
            LocationChanged += BaseEditForm_LocationChanged;
            SizeChanged += BaseEditForm_SizeChanged;
            void ControlEvents(Control control)
            {
                //control'ün keydown olayı olması gerekiyor
                control.KeyDown += Control_KeyDown;
                //Controle odaklandığında 
                control.GotFocus += Control_GotFocus;
                control.Leave += Control_Leave;

                switch (control)
                {
                    case FilterControl edt:
                        //EditValue Change ile aynı işlemi yapsın ...
                        edt.FilterChanged += Control_EditValueChanged;
                        break;
                    case ComboBoxEdit edt:
                        
                        edt.EditValueChanged += Control_EditValueChanged;   
                        edt.SelectedValueChanged += Control_SelectedValueChanged;
                        break;
                    case MyButtonEdit edt:
                        edt.IdChanged += Control_IdChanged;
                        edt.EnabledChange += Control_EnabledChange;
                        edt.ButtonClick += Control_ButtonClick;
                        edt.DoubleClick += Control_DoubleClick;
                        break;
                    //BaseEdit çoğu kontrolü kapsıyor , ButtonEdit,  TextEdit vs
                    //Bunların sırası önemli BaseEdit 'i alta yazıyoruz çünkü MyButtonEdit BaseEditten implemente
                    case BaseEdit edt:
                        edt.EditValueChanged += Control_EditValueChanged;
                        break;
                }
            }

            //Birden falza DataLayoutControls durumu oluştuğunda
            if (DataLayoutControls == null)
            {
                if (DataLayoutControl == null)
                {
                    return;
                }
                foreach (Control ctrl in DataLayoutControl.Controls)
                    ControlEvents(ctrl);
            }
            else
            {
                foreach (var layout in DataLayoutControls)
                    foreach (Control ctrl in layout.Controls)
                        ControlEvents(ctrl);
            }

        }


        //Tablo DÖküm işleminde bunu kullanacağız
        protected virtual void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        //Status Bar için işlemler
        private void Control_Leave(object sender, EventArgs e)
        {
            //Kontrolden ayrıldığı zaman 
            statusBarKisaYol.Visibility = BarItemVisibility.Never;
            statusBarKisaYolAciklama.Visibility = BarItemVisibility.Never;


        }

        private void Control_GotFocus(object sender, EventArgs e)
        {
            var type = sender.GetType();

            //Buton içeren kontroller 
            if (type == typeof(MyButtonEdit) || type == typeof(MyGridView) || type == typeof(MyPictureEdit) || type == typeof(MyComboBoxEdit) || type == typeof(MyDateEdit))
            {
                statusBarKisaYol.Visibility = BarItemVisibility.Always;
                statusBarKisaYolAciklama.Visibility = BarItemVisibility.Always;

                statusBarAciklama.Caption = ((IStatusBarAciklama)sender).StatusBarAciklama;

                statusBarKisaYol.Caption = ((IStatusBarKisaYol)sender).StatusBarKisaYol;
                statusBarKisaYolAciklama.Caption = ((IStatusBarKisaYol)sender).StatusBarKisaYolAciklama;
            }

            else if (sender is IStatusBarAciklama ctrl)
            {
                statusBarAciklama.Caption = ctrl.StatusBarAciklama;
            }
        }

        private void BaseEditForm_SizeChanged(object sender, EventArgs e)
        {
            _formSablonKayitEdilecek = true;
        }

        private void BaseEditForm_LocationChanged(object sender, EventArgs e)
        {
            _formSablonKayitEdilecek = true;
        }

        //Yapılan Değişiklikler kaydedilsinmi Eveti
        private void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Kapanmadan önce Kaydet Sorusu
            SablonKaydet();

            //Başka bir yerde yapılan işlemlerin Kaydedilmemesi gerekiyor ..
            if (btnKaydet.Visibility == BarItemVisibility.Never || !btnKaydet.Enabled)
            {
                return;
            }
            //true ise kapanacak -> False olması durumunda izin verme
            if (!Kaydet(true))
            {
                //Cancel durumunda kapanmayacak 
                e.Cancel = true;
            }
        }

        protected virtual void Control_EnabledChange(object sender, EventArgs e) { }

        private void Control_EditValueChanged(object sender, EventArgs e)
        {
            if (!IsLoaded) return;
            GuncelNesneOlustur();
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            SecimYap(sender);
        }

        private void Control_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SecimYap(sender);
        }

        private void Control_IdChanged(object sender, IdChangedEventArgs e)
        {
            if (!IsLoaded) return;
            GuncelNesneOlustur();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            //ESC basınca kapanması
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (sender is MyButtonEdit edt)
            {
                switch (e.KeyCode)
                {
                    //Shift Delete kombinasyonu
                    case Keys.Delete when e.Shift:
                        edt.Id = null;
                        edt.EditValue = null;
                        break;

                    case Keys.F4:
                        break;
                    //ALT aşağı ok tuşuyla kartları açıcak
                    case Keys.Down when e.Modifiers == Keys.Alt:
                        SecimYap(edt);
                        break;


                }
            }
        }

        private void BaseEditForm_Load(object sender, EventArgs e)
        {
            //Formun yüklenme aşamasında true
            IsLoaded = true;
            GuncelNesneOlustur();

            SablonYukle();
            ButonGizleGoster();

            //İşlem türüne Göre Id'ye değer atayacak EntityInsert ise
            //EskiEntity'e göre oluşturacak
            IslemTuru.IdOlustur(OldEntity);

            //güncelleme yapılacak



        }


        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Item == btnYeni)
            {
                //Yetki Kontrolü
                IslemTuru = IslemTuru.EntityInsert;
                Yukle();
            }
            else if (e.Item == btnKaydet)
            {
                Kaydet(false);
            }

            else if (e.Item == btnFarkliKaydet)
            {
                //Yetki Kontrolü
                FarkliKaydet();
            }
            else if (e.Item == btnGerial)
            {
                GeriAl();
            }
            else if (e.Item == btnSil)
            {
                //Yetki Kontrolü
                EntityDelete();
            }
            else if (e.Item == btnUygula)
            {
                FiltreUygula();
            }

            else if (e.Item == btnYazdir)
            {
                Yazdir();
            }

            else if (e.Item == btnBaskiOnizleme)
            {
                BaskiOnizleme();
            }


            else if (e.Item == btnCikis)
            {
                Close();
            }

            Cursor.Current = DefaultCursor;

        }

        protected virtual void BaskiOnizleme()
        {

        }

        protected virtual void Yazdir()
        {

        }

        private void FarkliKaydet()
        {
            if (Messages.EvetSeciliEvetHayir("Bu Filtre Referans Alınarak Yeni Bir Kayıt Oluşturulacaktır . Onaylıyor musunuz ?", "Kayıt Onay") != DialogResult.Yes)
            {
                return;

            }
            //Farklı kaydet ile inserte çevirecek ve yükle fonksiyonu çalışacak son olarak kayıt işlemi çalışacak ...
            IslemTuru = IslemTuru.EntityInsert;
            Yukle();
            if (Kaydet(true))
            {
                Close();
            }

        }

        protected virtual void FiltreUygula()
        {
            //
        }

        protected void SablonKaydet()
        {
            if (_formSablonKayitEdilecek)
            {
                //Formu Name özelliğinden yakalıyoruz
                Name.FormSablonKaydet(Left, Top, Width, Height, WindowState);

            }
        }

        private void SablonYukle()
        {
            //this ile gönderiyoruz
            Name.FormSablonYukle(this);
        }

        //Okul Edit formda kullanacağız


        private void ButonGizleGoster()
        {
            //Normal foreach ' de olabilir
            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
            HideItems?.ForEach(x => x.Visibility = BarItemVisibility.Never);

        }
        protected virtual void SecimYap(object sender) { }


        private void EntityDelete()
        {
            if (!((IBaseCommonBll)Bll).Delete(OldEntity)) return;
            //Başarılı olursa delete olacak Refresh Yap
            RefreshYapilacak = true;
            Close();

        }



        private void GeriAl()
        {
            //Tek bir yerde kullandıracağız ->Yes durumunda işlem türüüne bakılacak
            if (Messages.HayirSeciliEvetHayir("Yapılan Değişiklikler Geri Alınacaktır. Onaylıyor musunuz ?", "Geri Al Onay") != DialogResult.Yes)
            {
                return;
            }
            if (IslemTuru == IslemTuru.EntityUpdate)
            {
                Yukle();
            }
            else
            {
                Close();
            }
        }

        private bool Kaydet(bool kapanis)
        {
            bool KayitIslemi()
            {
                //Kaydederken Wait kısmı
                Cursor.Current = Cursors.WaitCursor;

                switch (IslemTuru)
                {
                    case IslemTuru.EntityInsert:
                        if (EntityInsert())
                            return KayitSonrasiIslemler();
                        break;

                    case IslemTuru.EntityUpdate:
                        if (EntityUpdate())
                            return KayitSonrasiIslemler();
                        break;

                }

                //Kaydetme'den sonra yapılacaklar
                bool KayitSonrasiIslemler()
                {
                    //CurrentValue old value olarak değişmesi lazım
                    OldEntity = CurrentEntity;
                    RefreshYapilacak = true;
                    ButonEnabledDurumu();

                    if (KayitSonrasiFormuKapat)
                        Close();
                    else
                    {
                        //Ekran Kapandıktan sonra Eski EntityInsert'ü tekrar Yeni giriş diye görmemesi için Update'e çekiyrouz
                        IslemTuru = IslemTuru == IslemTuru.EntityInsert ? IslemTuru.EntityUpdate : IslemTuru;
                    }
                    return true;
                }

                return false;
            }
            var result = kapanis ? Messages.KapanisMesaj() : Messages.KayitMesaj();

            switch (result)
            {

                case DialogResult.Yes:
                    return KayitIslemi();

                case DialogResult.No:
                    if (kapanis)
                        btnKaydet.Enabled = false;
                    return true;

                case DialogResult.Cancel:
                    //false durumunda kapanmayacak 
                    return false;

            }

            return false;

        }

        #region Insert Update
        protected virtual bool EntityUpdate()
        {
            return ((IBaseGenelBll)Bll).Update(OldEntity, CurrentEntity);
        }

        protected virtual bool EntityInsert()
        {

            return ((IBaseGenelBll)Bll).Insert(CurrentEntity);
        }
        #endregion



        //Override edeceğimiz zaman internal kısmını virtual yapacağız
        protected internal virtual void Yukle()
        {

        }

        //Entity oluşturup geriye döndürmek override ile kullanıcağız
        protected internal virtual IBaseEntity ReturnEntity()
        {
            return null;
        }


        protected virtual void NesneyiKontrollereBagla()
        {

        }

        protected virtual void GuncelNesneOlustur()
        {

        }

        protected internal virtual void ButonEnabledDurumu()
        {
            //Form Load' olduktan sonra tetikleme işlemlerine geçeceğiz ...
            if (!IsLoaded)
            {
                return;
            }
            else
            {
                //Buttonlarımızın enabled durumunu kontrol eden function
                GeneralFunctions.ButonEnabledDurumu(btnYeni, btnKaydet, btnGerial, btnSil, OldEntity, CurrentEntity);
                //Veriler Yüklenmiş ve ekranda gösterilmiş ise İşleme tabi tut.
            }
        }




    }
}