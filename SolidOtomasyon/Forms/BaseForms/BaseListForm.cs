using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Show.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities.Base;
using DevExpress.XtraPrinting.Native;
using SolidOtomasyon.Show;
using SolidOtomasyon.Forms.FiltreForms;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Forms.MainForms;
using DevExpress.Utils.Extensions;

namespace SolidOtomasyon.Forms.BaseForms
{
    public partial class BaseListForm : RibbonForm
    {

        #region Property'ler
        //Filtre için  seçili gelecek ID
        private long _filtreSeciliId;
        private bool _formSablonKayitEdilecek;
        private bool _tabloSablonKayitEdilecek;

        //Açılacak olan formu göndereceğiz
        protected IBaseFormShow FormShow;
        protected KartTuru BaseKartTuru;
        //Tabloları alacağız
        protected internal GridView Tablo;
        //Çoğu durumda default true gelecek
        protected bool AktifKartlariGoster = true;
        //Örnek Pasif Aktif Kartlar -> bazı yollarda True olacak
        protected internal bool AktifPasifButonGoster = false;
        //ShowEditForm'dan yapacağız o yüzden internal olacak
        protected internal bool MultiSelect;
        //Seçilen Kayıt - Row buraya atanacak
        protected internal BaseEntity SelectedEntity;
        //Temel Bll 
        protected IBaseBll Bll;
        protected internal long? SeciliGelecekId;

        protected ControlNavigator Navigator;

        //İtme gösterme ve gizleme dizi şeklinde birden fazla
        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;

        #endregion
        public BaseListForm()
        {
            InitializeComponent();

        }

        private void EventsLoad()
        {
            //Button Events
            foreach (BarItem button in ribbonControl.Items)
                button.ItemClick += Button_ItemClick;

            //Table Events
            Tablo.DoubleClick += Tablo_DoubleClick;
            Tablo.KeyDown += Tablo_KeyDown;
            Tablo.MouseUp += Tablo_MouseUp;
            Tablo.ColumnWidthChanged += Tablo_ColumnWidthChanged;
            Tablo.ColumnPositionChanged += Tablo_ColumnPositionChanged;
            Tablo.EndSorting += Tablo_EndSorting;
            Tablo.FilterEditorCreated += Tablo_FilterEditorCreated;
            Tablo.ColumnFilterChanged += Tablo_ColumnFilterChanged;

            //Form Events
            Shown += BaseListForm_Shown;
            Load += BaseListForm_Load;
            FormClosing += BaseListForm_FormClosing;
            LocationChanged += BaseListForm_LocationChanged;
            SizeChanged += BaseListForm_SizeChanged;
        }


        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Cursor durumunu wait ' olarak ayarla
            Cursor.Current = Cursors.WaitCursor;
            if (e.Item == btnGonder)
            {
                //Link'i açmamız lazım SubItem olduğunu bildiğimiz için cast ediyoruz
                var link = (BarSubItemLink)e.Item.Links[0];

                link.Focus();
                link.OpenMenu();
                //Açıldığında ilkine focuslanacak.
                link.Item.ItemLinks[0].Focus();

            }
            else if (e.Item == btnStandartExcellDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.ExcelStandart, e.Item.Caption, Text);
            }
            else if (e.Item == btnFormatliExcellDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.ExcelFormatli, e.Item.Caption, Text);
            }
            else if (e.Item == btnFormatsizExcellDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.ExcelFormatsiz, e.Item.Caption, Text);
            }
            else if (e.Item == btnWordDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.WordDosyasi, e.Item.Caption, Text);
            }
            else if (e.Item == btnPdfDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.PdfDosyasi, e.Item.Caption, Text);
            }
            else if (e.Item == btnMetinDosyasi)
            {
                Tablo.TabloDisariAktar(DosyaTuru.TxtDosyasi, e.Item.Caption, Text);
            }
            else if (e.Item == btnYeni)
            {
                // Yetki kontrolü olacka Yeni dosya olabilmek için

                //Yeni Form burada açılcak
                ShowEditForm(-1);
            }
            else if (e.Item == btnDuzelt)
            {
                //Odaklanan hücredeki değeri getir // id ye göre gelicek // Kolon ismi her tabloda farklı olabileceği için FieldName kullanacağız
                //Object Tip'te olduğu için Id için long'a cast ediyoruz.
                ShowEditForm(Tablo.GetRowId());
            }
            else if (e.Item == btnSil)
            {
                //Yetki Kontrolü
                EntityDelete();
            }
            else if (e.Item == btnSec)
            {
                SelectEntity();
            }
            else if (e.Item == btnYenile)
            {
                Listele();
            }
            else if (e.Item == btnFiltrele)
            {
                FiltreSec();
            }
            else if (e.Item == btnBagliKartlar)
            {
                BagliKartAc();
            }


            else if (e.Item == btnKolonlar)
            {
                //Özelleştirme Formu açacak
                if (Tablo.CustomizationForm == null)
                {
                    Tablo.ShowCustomization();
                }
                else
                {
                    Tablo.HideCustomization();
                }
            }

            else if (e.Item == btnYazdir)
            {
                Yazdir();
            }

            else if (e.Item == btnCikis)
            {
                Close();
            }
            else if (e.Item == btnAktifPasifKartlar)
            {
                //Aktif kartları gösterme durumunun zıttı ayarlandı
                AktifKartlariGoster = !AktifKartlariGoster;
                FormCaptionAyarlar();
            }

            //İşlemler bittikten sonra Default haline gelinecek.
            Cursor.Current = DefaultCursor;
        }

        private void Tablo_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IslemTuruSec();
            Cursor.Current = DefaultCursor;
        }

        private void Tablo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    IslemTuruSec();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
        private void BaseListForm_SizeChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
            {
                _formSablonKayitEdilecek = true;
            }
       
        }

        private void BaseListForm_LocationChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
            {
                _formSablonKayitEdilecek = true;
            }
        }

        private void Tablo_ColumnFilterChanged(object sender, EventArgs e)
        {
            //Filtreler devre dışı bırakıldığında filtre Id 0 olmaması lazım 
            if (string.IsNullOrEmpty(Tablo.ActiveFilterString))
            {
                _filtreSeciliId = 0;
            }
        }

        private void Tablo_FilterEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.FilterControlEventArgs e)
        {
            //DevExpres'in default filter editörü kapadık
            e.ShowFilterEditor = false;
            ShowEditForms<FiltreEditForm>.ShowDialogEditForm(KartTuru.Filtre, _filtreSeciliId, BaseKartTuru, Tablo.GridControl);
        }

        //Sıralama yaptığımızda'da kaydet
        private void Tablo_EndSorting(object sender, EventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void Tablo_ColumnPositionChanged(object sender, EventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void Tablo_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            _tabloSablonKayitEdilecek = true;
        }

        private void BaseListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SablonKaydet();
        }

        private void BaseListForm_Load(object sender, EventArgs e)
        {
            SablonYukle();
        }

        private void Tablo_MouseUp(object sender, MouseEventArgs e)
        {
            //sagTikMenu'muzu gonderiyoruz.
            e.SagTikMenuGoster(sagTikMenu);
        }

        private void BaseListForm_Shown(object sender, EventArgs e)
        {
            //Form ilk olarak yüklendiği zaman forum için tabloya focuslanmas lazım
            Tablo.Focus();

            ButonGizleGoster();

            //SablonYukle Fonksiyonu burada çalıştırsaydık -> ilk olarak default yerinde gelicekti sonradan xml değerlerini okuyup düzenleme yapıcaktı o yüzden LOAD Kısmında SablonYukleyi çağırdık

            //SutunGizleGoster();

            //General Funcdan gelicek
            //Field Name olarak Id string olarak yazılır , aranacak Değer ise SeciliGelecekId olarak gösteriyoruz
            //Son olarak kontrol null durumu HasValue ' ile değerinin olup olmadığını kontrol eder yok ise return
            if (IsMdiChild || !SeciliGelecekId.HasValue)
                return;

            Tablo.RowFocus("Id", SeciliGelecekId);
        }


        #region Form Metotlar
        private void ButonGizleGoster()
        {
            //Örnek Okul Kartları Kısmında -> Seç Butonu gözükmeyecek çünkü MdiChild şeklinde listeleniyor -> AktifPasifButon False ise gösterme
            btnSec.Visibility = AktifPasifButonGoster ? BarItemVisibility.Never : IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            //Aynı Zamanda Alttaki Seç texti 
            barEnter.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barEnterAciklama.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            //Aktif Pasif Kartlar buton görünürlüğü -> IsMdiChild değil ise gösterme diğer bütün durumlarda göster 
            btnAktifPasifKartlar.Visibility = AktifPasifButonGoster ? BarItemVisibility.Always : !IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;

            //Normal foreach ' de olabilir
            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
            HideItems?.ForEach(x => x.Visibility = BarItemVisibility.Never);


        }

        private void SutunGizleGoster()
        {
            throw new NotImplementedException();
        }

        private void SablonKaydet()
        {
            //True ise kaydet
            if (_formSablonKayitEdilecek)
            {
                Name.FormSablonKaydet(Left,Top,Width,Height,WindowState);
            }
            if(_tabloSablonKayitEdilecek)
            {
                Tablo.TabloSablonKaydet(IsMdiChild ? Name + " Tablosu" : Name + " TablosuMDI");
            }
        }

        private void SablonYukle()
        {
            if (IsMdiChild)
            {
                //Mdi ise Size kaydetmememiz gerekiyor çünkü tam ekran olarak açılacak her yerde
                Tablo.TabloSablonYukle(Name + " Tablosu");
            }
            else
            {
                //Üzerinde çalıştığımız form -> // MDI olması durumunda yani Child değil
                Name.FormSablonYukle(this);
                Tablo.TabloSablonYukle(Name + "TablosuMDI");
            }
        }

        protected internal void Yukle()
        {
            DegiskenleriDoldur();

            EventsLoad();

            //Sonradan kullanılacka
            Tablo.OptionsSelection.MultiSelect = MultiSelect;

            //Hangi Grid navigate olacak
            Navigator.NavigatableControl = Tablo.GridControl;

            //Listeleme işlemi sırasında
            Cursor.Current = Cursors.WaitCursor;
            Listele();
            Cursor.Current = DefaultCursor;

            //Guncellenicek

        }

        protected virtual void DegiskenleriDoldur() { }

        //Generic oluşturacağız ve Hangi Formu göstereceğini belirleyeceğiz.
        //ID alanına göre çekeceğiz
        //override edeceğiz ' İlçe için İl Id ve Il Adi göndereceğiz
        protected virtual void ShowEditForm(long id)
        {
            //Hangi formu açacağını bilmesi lazım -> Intrerfaceden ulaşacağız.
            var result = FormShow.ShowDialogEditForm(BaseKartTuru, id);
            //Ayrı kullanacağımız için farklı fonksiyon içine yazdık
            ShowEditFormDefault(result);
        }

        //Ekleme işleminden sonra formun kapanması ve o değere focuslanması
        protected void ShowEditFormDefault(long id)
        {
            if (id <= 0)
                return;
            AktifKartlariGoster = true;
            FormCaptionAyarlar();
            Tablo.RowFocus("Id", id);

        }


        protected virtual void EntityDelete()
        {
            var entity = Tablo.GetRow<BaseEntity>();
            if (entity == null)
            {
                return;
            }
            //Bll'den delete function ulaşmak lazım // Başarısızsa return yap
            if (!((IBaseCommonBll)Bll).Delete(entity))
            {
                return;
            }
            Tablo.DeleteSelectedRows();
            //Silme sonrası focus işlemi
            Tablo.RowFocus(Tablo.FocusedRowHandle);
        }

        private void SelectEntity()
        {
            //MultiSelect var ise çeşitli eklemeler yapılacak
            if (MultiSelect)
            {
                //Güncellenecek
            }
            else
            {
                //Seçilen bilgi satır bir yere kaydolması gerekiyor
                SelectedEntity = Tablo.GetRow<BaseEntity>();
            }

            DialogResult = DialogResult.OK;
            Close();

        }
        protected virtual void Listele() { }


        private void FiltreSec()
        {
            //son 2 parametre Uygulanacağı kart türü ve uygulanacağı grid
            var entity = (Filtre)ShowListForms<FiltreListForm>.ShowDialogListForm(KartTuru.Filtre, _filtreSeciliId,BaseKartTuru,Tablo.GridControl);

            if (entity == null)
            {
                return;
            }

            _filtreSeciliId = entity.Id;
            Tablo.ActiveFilterString = entity.FiltreMetni;

        }

        protected virtual void Yazdir()
        {
            TablePrintingFunctions.Yazdir(Tablo,Tablo.ViewCaption,AnaForm.SubeAdi);
        }

        private void FormCaptionAyarlar()
        {
            //Aktif pasif kartlar ayar
            if (btnAktifPasifKartlar == null)
            {
                //Pasif kartların bazı durumlarda gözükmemesi için null durumları oluşabiliyor
                Listele();
                return;
            }

            if (AktifKartlariGoster)
            {
                btnAktifPasifKartlar.Caption = "Pasif Kartlar";
                //Örnek : OKUL KARTLARI
                Tablo.ViewCaption = Text;
            }
            else
            {
                btnAktifPasifKartlar.Caption = "Aktif Kartlar";
                //Tablonun üst Kısmında Ek Olarak Pasif Kartlar olarak Belirticek  Örnek : OKUL KARTLARI Pasif Kartlar
                Tablo.ViewCaption = Text + " - Pasif Kartlar";
            }

            Listele();
        }

        private void IslemTuruSec()
        {
            // Ana Formun içindeki Açılan form MdiChild Form olarak geçer
            if (!IsMdiChild)
            {
                // Bu Kısım Güncellenicektir.
                SelectEntity();
            }
            else
            {
                btnDuzelt.PerformClick();
            }
        }

        protected virtual void BagliKartAc()
        {

        }

        #endregion





    }
}