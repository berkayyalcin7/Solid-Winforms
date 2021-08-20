using DevExpress.XtraEditors;
using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Dto;
using SolidOtomasyon.Takip.Model.Entities;
using System;

namespace SolidOtomasyon.Forms.OkulForms
{
    public partial class OkulEditForm : BaseEditForm
    {
        public OkulEditForm()
        {
            InitializeComponent();

            DataLayoutControl = myDataLayoutControl;

            //MyDataLayoutControl göndereceğiz : Gerekli Hataları hangi controlde olup bulması için bunu göndermesi lazım.
            Bll = new OkulBll(myDataLayoutControl);

            BaseEditKartTuru = KartTuru.Okul;

            //
            EventsLoad();
        }

        protected internal override void Yukle()
        {
            // Insert veya Update Olması durumunu kontrol ediyoruz Insert İse
            // Dto'dan instance alsın Update ' ise Single'ile çek getir.
            OldEntity = IslemTuru == IslemTuru.EntityInsert ? new OkulS() : ((OkulBll)Bll).Single(FilterFunctions.Filter<Okul>(Id));
            NesneyiKontrollereBagla();

            //Entity insert olmadığı sürece çalıştırma
            if (IslemTuru != IslemTuru.EntityInsert) return;
            Id = IslemTuru.IdOlustur(OldEntity);
            //Txt'kod kısmına yeni kod oluşturup atacağız ... 
            txtKod.Text = ((OkulBll)Bll).YeniKodVer();
            txtOkulAdi.Focus();
        }

        protected override void NesneyiKontrollereBagla()
        {
            //Gelen ' entityle kontrollere bağlayacağız
            //OkulS'ye cast etme nedeni içindeki İl İlçe Bunları da kullanabilmek için
            var entity = (OkulS)OldEntity;


            txtKod.Text = entity.Kod;
            txtOkulAdi.Text = entity.OkulAdi;
            txtIl.Id = entity.IlId;
            txtIl.Text = entity.IlAdi;
            txtIlce.Id = entity.IlceId;
            txtIlce.Text = entity.IlceAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }

        protected override void GuncelNesneOlustur()
        {
            //Db'ye gönderilen entity'den instance -> Gönderilen Değişiklikleri yakalayabileceğiz
            CurrentEntity = new Okul
            {
                Id = Id,
                Kod = txtKod.Text,
                OkulAdi = txtOkulAdi.Text,
                //(long) ' cast etseydik silme durumunda oluşabilecek hatalar olur
                IlId = Convert.ToInt64(txtIl.Id),
                IlceId = Convert.ToInt64(txtIlce.Id),
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn


            };
            //İşlemi son olarak Function çağırarak tamamlıyoruz ...
            ButonEnabledDurumu();

        }

        protected override void SecimYap(object sender)
        {
            if (!(sender is ButtonEdit))
            {
                //herhangi bir işlem yapma
                return;
            }
            using (var sec = new SelectFunctions())
            {
                if (sender == txtIl)

                    sec.Sec(txtIl);
                else if (sender == txtIlce)
                    //prm olarak txtIl göndereceğiz
                    sec.Sec(txtIlce, txtIl);
            }
        }

        protected override void Control_EnabledChange(object sender, EventArgs e)
        {
            if (sender !=txtIl) return;

            txtIl.ControlEnabledChange(txtIlce);
        }


    }
}