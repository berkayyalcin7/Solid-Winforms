using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    public class VeliBilgiBll : BaseGenelBll<VeliBilgi>, IBaseGenelBll, IBaseCommonBll
    {
        public VeliBilgiBll() : base(KartTuru.VeliBilgi){}

        public VeliBilgiBll(Control ctrl) : base(ctrl, KartTuru.VeliBilgi){}
    }
}
