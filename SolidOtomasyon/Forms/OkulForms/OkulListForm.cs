using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.OkulForms
{
    //BaseKart Bize daha az kod yazma kolaylığı sağlayacak.
    public partial class OkulListForm : BaseListForm
    {
        public OkulListForm()
        {
            InitializeComponent();
            Bll = new OkulBll();
            ////Geçici Olarak Vt'nin oluşması için tetikliyoruz.
            //OkulBll bll = new OkulBll();

            ////Gerekli Model Referansını ekliyoruz
            //grid.DataSource = bll.List(null);

        }

        // İçerikler doldurulacak -> BaseListForm'a gönderilecek değerler burada dolacak Tablo vs 
        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            //BaseListForm'dan gelen
            BaseKartTuru = KartTuru.Okul;
            //Double Click' olayında OkulEditForm açılacak
            FormShow = new ShowEditForms<OkulEditForm>();
            Navigator = longNavigator1.Navigator;

        }

        protected override void Listele()
        {
            //List İçerisine filtre göndereceğiz
            Tablo.GridControl.DataSource = ((OkulBll)Bll).List(FilterFunctions.Filter<Okul>(AktifKartlariGoster));
        }

    }
}