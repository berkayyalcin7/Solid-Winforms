using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.BLL.Interfaces
{
    //OkulBll'de tanımladığımız Insert ve Update
    public interface IBaseGenelBll
    {
        bool Insert(BaseEntity entity);

        bool Update(BaseEntity oldEntity, BaseEntity currentEntity);

        string YeniKodVer();
    }
}
