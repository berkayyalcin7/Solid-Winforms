using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.BLL.Interfaces
{
    //Ortak anlamında Common BLL oluşuyor -> yani İl İlçe Okul'da kullanabilecek -> Örn : Silme işlemi
    public interface IBaseCommonBll
    {
        bool Delete(BaseEntity entity);


    }
}
