using DevExpress.XtraBars;
using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.VeliBilgiForms
{
    public partial class VeliBilgiListForm : BaseListForm
    {
        public VeliBilgiListForm()
        {
            InitializeComponent();

            Bll = new VeliBilgiBll();


        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.VeliBilgi;
            FormShow = new ShowEditForms<VeliBilgiEditForm>();
            Navigator = longNavigator.Navigator;

        }

        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((VeliBilgiBll)Bll).List(FilterFunctions.Filter<VeliBilgi>(AktifKartlariGoster));
        }
    }
}