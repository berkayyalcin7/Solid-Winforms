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

namespace SolidOtomasyon.Forms.IlceForms
{
    public partial class IlceEditForm : BaseEditForm
    {

        #region değişkenler
        private readonly long _ilId;
        private readonly string _ilAdi;
        #endregion


        //Parametre tipini bilmediğimiz için object hepsini kapsasın diye
        public IlceEditForm(params object[] prm)
        {
            InitializeComponent();

            _ilId = (long)prm[0];
            _ilAdi = prm[1].ToString();

            DataLayoutControl = myDataLayoutControl;

            //MyDataLayoutControl göndereceğiz : Gerekli Hataları hangi controlde olup bulması için bunu göndermesi lazım.
            Bll = new IlceBll(myDataLayoutControl);

            BaseEditKartTuru = KartTuru.Ilce;

            EventsLoad();

        }

        protected internal override void Yukle()
        {
            // Insert veya Update Olması durumunu kontrol ediyoruz Insert İse
            // Dto'dan instance alsın Update ' ise Single'ile çek getir. -> (Not DTO 'yu Okulda kullandık burada Il'i direkt olarak kullanacağız)
            OldEntity = IslemTuru == IslemTuru.EntityInsert ? new Ilce() : ((IlceBll)Bll).Single(FilterFunctions.Filter<Ilce>(Id));
            NesneyiKontrollereBagla();
            // İlçe Kartları - ( Adana ) Örnek
            Text = Text + $" - ( {_ilAdi} )";
            //Entity insert olmadığı sürece çalıştırma
            if (IslemTuru != IslemTuru.EntityInsert) return;
            Id = IslemTuru.IdOlustur(OldEntity);
            //Txt'kod kısmına yeni kod oluşturup atacağız ...  -> IL ' ID' ye göre bi kod vereceğiz
            txtKod.Text = ((IlceBll)Bll).YeniKodVer(x => x.IlId == _ilId);
            txtIlceAdi.Focus();
        }

        protected override void NesneyiKontrollereBagla()
        {
            //Gelen ' entityle kontrollere bağlayacağız
            //Ilce
            var entity = (Ilce)OldEntity;
            txtKod.Text = entity.Kod;
            txtIlceAdi.Text = entity.IlceAdi;
            txtAciklama.Text = entity.Aciklama;
            tglDurum.IsOn = entity.Durum;
        }

        protected override void GuncelNesneOlustur()
        {
            //Db'ye gönderilen entity'den instance -> Gönderilen Değişiklikleri yakalayabileceğiz
            CurrentEntity = new Ilce
            {
                Id = Id,
                Kod = txtKod.Text,
                IlceAdi = txtIlceAdi.Text,
                //Ilçenin Il'id si bulunmak zorunda
                IlId = _ilId,
                Aciklama = txtAciklama.Text,
                Durum = tglDurum.IsOn


            };
            //İşlemi son olarak Function çağırarak tamamlıyoruz ...
            ButonEnabledDurumu();


        }

        protected override bool EntityInsert()
        {
            //Kod şimdiki koda eşit ise ve il'id ile gelen parametre eşit ise
            return ((IlceBll)Bll).Insert(CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.IlId == _ilId);
        }

        protected override bool EntityUpdate()
        {
            //Update işleminde old ve current kısmını test ediyoruz.
            return ((IlceBll)Bll).Update(OldEntity, CurrentEntity, x => x.Kod == CurrentEntity.Kod && x.IlId == _ilId);
        }
    }
}