using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Linq.Expressions;

namespace SolidOtomasyon.Functions
{
    public class FilterFunctions
    {
        public static Expression<Func<T, bool>> Filter<T>(bool aktifKartlariGoster) where T : BaseEntityDurum
        {
            //Filtre oluşturmuş olduk.
            return x => x.Durum == aktifKartlariGoster;
        }

        public static Expression<Func<T, bool>> Filter<T>(long id) where T : BaseEntity
        {
            return x => x.Id == id;
        }



    }
}
