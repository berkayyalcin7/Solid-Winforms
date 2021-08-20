using DevExpress.XtraBars;
using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Forms.IlceForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System.Linq;

namespace SolidOtomasyon.Forms.IlForms
{
    public partial class IlListForm : BaseListForm
    {
        public IlListForm()
        {
            InitializeComponent();


            Bll = new IlBll();
            btnBagliKartlar.Caption = "İlçe Kartları";

        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Il;
            FormShow = new ShowEditForms<IlEditForm>();
            Navigator = longNavigator1.Navigator;

            if (IsMdiChild)
            {
                ShowItems = new BarItem[] { btnBagliKartlar  };
            }
            
        }

        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((IlBll)Bll).List(FilterFunctions.Filter<Il>(AktifKartlariGoster));
        }

        protected override void BagliKartAc()
        {
            // Yetki Kontrolü daha sonra olacak

            var entity = tablo.GetRow<Il>();
            if (entity==null)
            {
                return;
            }
            //İlçe list açarken
            ShowListForms<IlceListForm>.ShowListForm(KartTuru.Ilce,entity.Id,entity.IlAdi);
        }
    }
}