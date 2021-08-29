using SolidOtomasyon.BLL.General;
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Functions;
using SolidOtomasyon.Show;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;

namespace SolidOtomasyon.Forms.IptalNedeniForms
{
    public partial class IptalNedeniListForm : BaseListForm
    {
        public IptalNedeniListForm()
        {
            InitializeComponent();
            Bll = new IptalNedeniBll();

        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.IptalNedeni;
            FormShow = new ShowEditForms<IptalNedeniEditForm>();
            Navigator = longNavigator.Navigator;

        }

        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((IptalNedeniBll)Bll).List(FilterFunctions.Filter<IptalNedeni>(AktifKartlariGoster));
        }
    }
}