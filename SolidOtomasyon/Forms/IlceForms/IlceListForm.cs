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
using SolidOtomasyon.Show;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.IlceForms
{
    public partial class IlceListForm : BaseListForm
    {
        //Parametresiz kullanım olmayacak
        //public IlceListForm()
        //{


        //}

        #region değişkenler
        private readonly long _ilId;
        private readonly string _ilAdi;

        #endregion
        public IlceListForm(params object[] prm)
        {
            InitializeComponent();

            //İlçe List Forma veri çekerken İl'e ihtiyacımız var
            //İl 'Id ve İl Adı Göndereceğiz parametreye
            Bll = new IlceBll();
            //gelen ilk parametreye 0. indeks
            _ilId = (long)prm[0];
            //1. indeks 2. değer il adı olacak
            _ilAdi = prm[1].ToString();


        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Ilce;

            //Burada override ederek bu işlemi yapacağımız için (İl Adi ve ID'si gerekiyor bu satır kapalı kalacak ...)
            //FormShow = new ShowEditForms<IlceEditForm>();


            Navigator = longNavigator1.Navigator;
            //İlçe seçildiğinde en üstte il Adı nın'da çıkması  gerekiiyor
            Text = Text + $" - ( {_ilAdi} )";


        }

        protected override void Listele()
        {
            //Özel bi sorgu getirmemiz gerek İl id'sine göre ve aktif kartları göster diğer durumlarda FilterFunctions'ı kullanabiliriz
            Tablo.GridControl.DataSource = ((IlceBll)Bll).List(x => x.Durum == AktifKartlariGoster && x.IlId == _ilId);
        }

        protected override void ShowEditForm(long id)
        {
            var result = ShowEditForms<IlceEditForm>.ShowDialogEditForm(KartTuru.Ilce, id, _ilId, _ilAdi);
            //Burada işlem yapılacak ... 
            ShowEditFormDefault(result);
        }

        


    }
}