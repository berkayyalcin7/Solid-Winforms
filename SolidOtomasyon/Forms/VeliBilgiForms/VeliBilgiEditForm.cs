using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Functions;

namespace SolidOtomasyon.Forms.VeliBilgiForms
{
    public partial class VeliBilgiEditForm : BaseEditForm
    {
        public VeliBilgiEditForm()
        {
            InitializeComponent();

            DataLayoutControl = myDataLayoutControl;

            Bll = new VeliBilgiBll(myDataLayoutControl);

            BaseEditKartTuru = KartTuru.VeliBilgi;

            EventsLoad();

        }

        protected internal override void Yukle()
        {
            // Insert veya Update Olması durumunu kontrol ediyoruz Insert İse
            // Dto'dan instance alsın Update ' ise Single'ile çek getir. -> (Not DTO 'yu Okulda kullandık burada Il'i direkt olarak kullanacağız)
            OldEntity = IslemTuru == IslemTuru.EntityInsert ? new VeliBilgi() : ((VeliBilgiBll)Bll).Single(FilterFunctions.Filter<VeliBilgi>(Id));
            NesneyiKontrollereBagla();

            //Entity insert olmadığı sürece çalıştırma
            if (IslemTuru != IslemTuru.EntityInsert) return;
            Id = IslemTuru.IdOlustur(OldEntity);
            //Txt'kod kısmına yeni kod oluşturup atacağız ... 
            txtKod.Text = ((VeliBilgiBll)Bll).YeniKodVer();
            txtBilgiAdi.Focus();
        }

        protected override void NesneyiKontrollereBagla()
        {
            var entity = (VeliBilgi)OldEntity;


            txtKod.Text = entity.Kod;
            txtBilgiAdi.Text = entity.BilgiAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }

        protected override void GuncelNesneOlustur()
        {
            CurrentEntity = new VeliBilgi
            {
                Id = Id,
                Kod = txtKod.Text,
                BilgiAdi = txtBilgiAdi.Text,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn
            };

            ButonEnabledDurumu();

        }
    }

}