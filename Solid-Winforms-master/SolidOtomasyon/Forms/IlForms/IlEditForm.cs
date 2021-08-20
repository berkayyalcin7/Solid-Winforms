using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.IlForms
{
    public partial class IlEditForm : BaseEditForm
    {
        public IlEditForm()
        {
            InitializeComponent();

            DataLayoutControl = myDataLayoutControl;

            //MyDataLayoutControl göndereceğiz : Gerekli Hataları hangi controlde olup bulması için bunu göndermesi lazım.
            Bll = new IlBll(myDataLayoutControl);

            BaseEditKartTuru = KartTuru.Il;

            //
            EventsLoad();
        }

        protected internal override void Yukle()
        {
            // Insert veya Update Olması durumunu kontrol ediyoruz Insert İse
            // Dto'dan instance alsın Update ' ise Single'ile çek getir. -> (Not DTO 'yu Okulda kullandık burada Il'i direkt olarak kullanacağız)
            OldEntity = IslemTuru == IslemTuru.EntityInsert ? new Il() : ((IlBll)Bll).Single(FilterFunctions.Filter<Il>(Id));
            NesneyiKontrollereBagla();

            //Entity insert olmadığı sürece çalıştırma
            if (IslemTuru != IslemTuru.EntityInsert) return;
            Id=IslemTuru.IdOlustur(OldEntity);
            //Txt'kod kısmına yeni kod oluşturup atacağız ... 
            txtKod.Text = ((IlBll)Bll).YeniKodVer();
            txtIlAdi.Focus();
        }

        protected override void NesneyiKontrollereBagla()
        {
            //Gelen ' entityle kontrollere bağlayacağız
            //Il
            var entity = (Il)OldEntity;


            txtKod.Text = entity.Kod;
            txtIlAdi.Text = entity.IlAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }

        protected override void GuncelNesneOlustur()
        {
            //Db'ye gönderilen entity'den instance -> Gönderilen Değişiklikleri yakalayabileceğiz
            CurrentEntity = new Il
            {
                Id = Id,
                Kod = txtKod.Text,
                IlAdi = txtIlAdi.Text,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn


            };
            //İşlemi son olarak Function çağırarak tamamlıyoruz ...
            ButonEnabledDurumu();

    
        }

        private void IlEditForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}