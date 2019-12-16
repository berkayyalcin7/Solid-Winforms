using SolidOtomasyon.BLL.Base;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Data.Contexts;
using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    //IBaseGenelBll'i burada kullanmıyoruz.
    public class IlceBll : BaseBll<Ilce, OgrenciTakipContext> ,IBaseCommonBll
    {
        public IlceBll()
        {
        }

        public IlceBll(Control ctrl) : base(ctrl) { }

        public BaseEntity Single(Expression<Func<Ilce, bool>> filter)
        {
            //Seçim işlemini selector burada devreye giricek fonksiyon parametresinde değil.
            //Burada Selector için Data Transfer Object oluşturmadan X ' i getir

            return BaseSingle(filter, x => x);
          
        }
   

        public IEnumerable<BaseEntity> List(Expression<Func<Ilce, bool>> filter)
        {
            //BaseList IQueryable türünde döndüğü için bunu ekrana getirirken sıralama ve Listeleme yapıyoruz.
            //Burada ToList() döndürmemizin sebebi OrderBy ile filtre uygulamamız . Yani İlk Sıralama Sonra Listeleme işlemi
            return BaseList(filter, x => x).OrderBy(x=>x.Kod).ToList();
           
        }


        //Ilcelerin Listesini alabilmek için İl'leri almamız gerekiyor

        public bool Insert(BaseEntity entity, Expression<Func<Ilce, bool>> filter)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan


            return BaseInsert(entity, filter);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<Ilce, bool>> filter)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseUpdate(oldEntity, currentEntity, filter);
        }

        public bool Delete(BaseEntity entity)
        {

            return BaseDelete(entity, KartTuru.Ilce);

        }

        public string YeniKodVer(Expression<Func<Ilce, bool>> filter)
        {
            //tür ve filter verdik -> Kod alanınını getir
            return BaseYeniKodVer(KartTuru.Ilce, x => x.Kod,filter);
        }
    }
}
