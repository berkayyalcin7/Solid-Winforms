using DevExpress.XtraBars;
using SolidOtomasyon.Forms.IlForms;
using SolidOtomasyon.Forms.OkulForms;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;

namespace SolidOtomasyon.Forms.MainForms
{
    public partial class AnaForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public static string DonemAdi = "Dönem Bilgisi Bekleniyor...";
        public static string SubeAdi = "Şube Bilgisi Bekleniyor...";
        public AnaForm() 
        {
            InitializeComponent();
            //Çalışması için Constructora ekliyoruz.
            EventsLoad();
        }

        //Event Load edildiğinde
        private void EventsLoad()
        {
            //Header menüdeki Ribborn Control de dolaşıyoruz
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarButtonItem btn:
                        btn.ItemClick += Butonlar_ItemClick;  
                        break;


                }

            }
        }

        //Event Fırlatmış olduk
        private void Butonlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.Item == btnOkulKartlari)
            {
                ShowListForms<OkulListForm>.ShowListForm(KartTuru.Okul);
            }
            else if(e.Item==btnIlKartlari)
            {
                ShowListForms<IlListForm>.ShowListForm(KartTuru.Il);
            }
        }
    }
}