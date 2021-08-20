using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.FiltreForms
{
    public partial class FiltreEditForm : BaseEditForm
    {

        #region değişkenler
        private readonly KartTuru _filtreKartTuru;
        private readonly GridControl _filtreGrid;
        #endregion


        public FiltreEditForm(params object[] prm)
        {

            InitializeComponent();

            _filtreKartTuru = (KartTuru)prm[0];
            _filtreGrid = (GridControl)prm[1];

      
            //Edit Formda Yeni ve Geri Al kısmı olmayacaktır List tarafında olaacak..
            HideItems = new BarItem[] { btnYeni, btnGerial };
            ShowItems = new BarItem[] { btnFarkliKaydet, btnUygula };


            DataLayoutControl = myDataLayoutControl;
            //MyDataLayoutControl göndereceğiz : Gerekli Hataları hangi controlde olup bulması için bunu göndermesi lazım.
            Bll = new FiltreBll(myDataLayoutControl);
            BaseEditKartTuru = KartTuru.Ilce;
            EventsLoad();
        }


        protected internal override void Yukle()
        {
            txtFiltreMetni.SourceControl = _filtreGrid;

            while (true)
            {
                if (IslemTuru == IslemTuru.EntityInsert)
                {
                    OldEntity = new Filtre();
                    Id = IslemTuru.IdOlustur(OldEntity);
                    // Örnek Okul Geliyorsa -> Okul için Filtre kodu
                    txtKod.Text = ((FiltreBll)Bll).YeniKodVer(x => x.KartTuru == _filtreKartTuru);

                }

                else
                {
                    // Filter fonksiyonu BaseEntity'den implemente oluyor 
                    OldEntity = ((FiltreBll)Bll).Single(FilterFunctions.Filter<Filtre>(Id));
                    if (OldEntity == null)
                    {

                        //Filtreyi bulamıyorsak Inserte çevir 
                        IslemTuru = IslemTuru.EntityInsert;
                        //Tabloya uygulanan filtre silinirse Inserte çevir ve başa dön
                        continue;
                    }
                    NesneyiKontrollereBagla();
                }
                //İşlemi burada durdur
                break;
            }

        }

        protected override void NesneyiKontrollereBagla()
        {
            //Gelen ' entityle kontrollere bağlayacağız
            //Ilce
            var entity = (Filtre)OldEntity;
            txtKod.Text = entity.Kod;
            txtFiltreAdi.Text = entity.FiltreAdi;
            txtFiltreMetni.FilterString = entity.FiltreMetni;
        }

        protected override void GuncelNesneOlustur()
        {
            //Db'ye gönderilen entity'den instance -> Gönderilen Değişiklikleri yakalayabileceğiz
            CurrentEntity = new Filtre
            {
                Id = Id,
                Kod = txtKod.Text,
                FiltreAdi = txtFiltreAdi.Text,
                //Text olarak değill filter string olarak düzenliyoruz .
                FiltreMetni= txtFiltreMetni.FilterString,
                //Hangi karta ait olduğu bilinmesi lazım
                KartTuru=_filtreKartTuru
            };
            //İşlemi son olarak Function çağırarak tamamlıyoruz ...
            ButonEnabledDurumu();


        }
        protected override bool EntityInsert()
        {
            //Kod şimdiki koda eşit ise ve kart türü ile gelen parametre eşit ise
            return ((FiltreBll)Bll).Insert(CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.KartTuru==_filtreKartTuru);
        }

        protected override bool EntityUpdate()
        {
            //Update işleminde old ve current kısmını test ediyoruz.
            return ((FiltreBll)Bll).Update(OldEntity, CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.KartTuru == _filtreKartTuru);

        }

        protected override void FiltreUygula()
        {
            //Filtre Metni Seçme
            txtFiltreMetni.Select();
            //Filtreyi uygulama
            txtFiltreMetni.ApplyFilter();
        }

        protected internal override void ButonEnabledDurumu()
        {
            if (!IsLoaded)
            {
                return;
            }

            GeneralFunctions.ButonEnabledDurumu(btnKaydet, btnFarkliKaydet, btnSil, IslemTuru, OldEntity, CurrentEntity);
        }


    }
}