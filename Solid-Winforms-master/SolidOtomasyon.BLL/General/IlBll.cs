using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    public class IlBll : BaseGenelBll<Il>, IBaseGenelBll , IBaseCommonBll
    {
        /// <summary>
        /// Bütün Fonksiyonlara BaseGenelBll'den ulaşıyoruz ... 
        /// </summary>

        public IlBll():base(KartTuru.Il)
        {
        }

        public IlBll(Control ctrl) : base(ctrl,KartTuru.Il) { }

        //DTO oluşturmadığımız için Single ve List Kısımlarınıda Kullanmıyoruz

      

    }
}
