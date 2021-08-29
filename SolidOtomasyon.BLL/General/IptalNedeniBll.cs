using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    public class IptalNedeniBll : BaseGenelBll<IptalNedeni>, IBaseGenelBll, IBaseCommonBll
    {
        public IptalNedeniBll():base(KartTuru.IptalNedeni)
        {
        }

        public IptalNedeniBll(Control ctrl):base(ctrl,KartTuru.IptalNedeni)
        {
        }




    }
}
