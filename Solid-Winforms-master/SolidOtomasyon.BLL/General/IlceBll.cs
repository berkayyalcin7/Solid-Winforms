using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    //IBaseGenelBll'i burada kullanmıyoruz. Çünkü : IlceBll 'deki Insert Update ve Yenikodver alanı Filtre alıyor ve IBaseGenelBll'e uymuyor ...
    public class IlceBll : BaseGenelBll<Ilce> ,IBaseCommonBll
    {
        public IlceBll():base(KartTuru.Ilce)
        {
        }

        public IlceBll(Control ctrl) : base(ctrl,KartTuru.Ilce) { }

       

      
    }
}
