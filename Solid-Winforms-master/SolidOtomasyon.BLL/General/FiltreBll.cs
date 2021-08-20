using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    public class FiltreBll : BaseGenelBll<Filtre>, IBaseCommonBll
    {
        public FiltreBll():base(KartTuru.Filtre) { }

        public FiltreBll(Control ctrl):base(ctrl,KartTuru.Filtre) { }


    
    }
}
