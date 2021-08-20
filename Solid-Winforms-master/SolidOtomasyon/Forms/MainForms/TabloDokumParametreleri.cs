using System;
using DevExpress.XtraBars;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Functions;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;

namespace SolidOtomasyon.Forms.MainForms
{
    public partial class TabloDokumParametreleri : BaseEditForm
    {

        #region Değişkenler

        private DokumSekli _dokumSekli;
        //constructorda atayacağımız için readonly yaptık
        private readonly string _raporBaslik;

        #endregion
        public TabloDokumParametreleri(params object[] prm)
        {
            InitializeComponent();

            //2. Olan DataLayoutControl TabloDokumdeki Form alanı
            DataLayoutControl = myDataLayoutControl2;
            HideItems = new BarItem[] { btnYeni, btnKaydet, btnGerial, btnSil };
            ShowItems = new BarItem[] { btnYazdir, btnBaskiOnizleme };
            EventsLoad();
            //İlk parametremiz Rapor Başlığımızdı
            _raporBaslik = prm[0].ToString();

        }


        protected internal override void Yukle()
        {
            txtRaporBasligi.Text = _raporBaslik;
            //Baslik Ekle kısmı Evet Hayır şeklinde
            txtBaslikEkle.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<EvetHayir>());
            txtRaporKagidaSigdir.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<RaporuKagidaSigdirma>());
            txtYazdirmaYonu.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<YazdirmaYonu>());
            txtYatayCizgileriGoster.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<EvetHayir>());
            txtDikeyCizgileriGoster.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<EvetHayir>());
            txtSutunBasliklariGoster.Properties.Items.AddRange(EnumFunctions.GetEnumDescriptionList<EvetHayir>());

            txtYaziciAdi.Properties.Items.AddRange(GeneralFunctions.YazicilariListele());

            //Default gelecekler ToName() ile Enumların Descriptionlarına ulaşıyoruz
            txtBaslikEkle.SelectedItem = EvetHayir.Evet.ToName();
            txtRaporKagidaSigdir.SelectedItem = RaporuKagidaSigdirma.YaziBoyutunuKuculterekSigdir.ToName();         
            txtYazdirmaYonu.SelectedItem = YazdirmaYonu.Otomatik.ToName();
            txtYatayCizgileriGoster.SelectedItem = EvetHayir.Evet.ToName();
            txtDikeyCizgileriGoster.SelectedItem = EvetHayir.Evet.ToName();
            txtSutunBasliklariGoster.SelectedItem = EvetHayir.Evet.ToName();
            //Default Yazıcı gelmesi gerekiyor 
            txtYaziciAdi.EditValue = GeneralFunctions.DefaultYaziciGetir();
        }

        protected internal override IBaseEntity ReturnEntity()
        {
            var entity = new DokumParametreleri {
                RaporBaslik = txtRaporBasligi.Text,
                BaslikEkle = txtBaslikEkle.Text.GetEnum<EvetHayir>(),
                RaporuKagidaSigdir = txtRaporKagidaSigdir.Text.GetEnum<RaporuKagidaSigdirma>(),
                YazdirmaYonu = txtYazdirmaYonu.Text.GetEnum<YazdirmaYonu>(),
                YatayCizgileriGoster = txtYatayCizgileriGoster.Text.GetEnum<EvetHayir>(),
                DikeyCizgileriGoster = txtDikeyCizgileriGoster.Text.GetEnum<EvetHayir>(),
                SutunBasliklariniGoster = txtSutunBasliklariGoster.Text.GetEnum<EvetHayir>(),
                YaziciAdi = txtYaziciAdi.Text,
                YazdirilacakAdet = (int)txtYazdirilacakAdet.Value,
                DokumSekli = _dokumSekli

           
            };

            return entity;
        }

        protected override void Yazdir()
        {
            _dokumSekli = DokumSekli.TabloYazdir;
            Close();
        }

        protected override void BaskiOnizleme()
        {
            _dokumSekli = DokumSekli.TabloBaskiOnizleme;

            Close();
        }

        protected override void Control_SelectedValueChanged(object sender, EventArgs e)
        {
            //Başlık Ekle Evet ise Enabled yap
            txtRaporBasligi.Enabled = txtBaslikEkle.Text.GetEnum<EvetHayir>() == EvetHayir.Evet;
        }
    }
}