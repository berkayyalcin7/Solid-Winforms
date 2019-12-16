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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.General
{
    public class IlBll : BaseBll<Il, OgrenciTakipContext>, IBaseGenelBll , IBaseCommonBll
    {
        public IlBll()
        {
        }

        public IlBll(Control ctrl) : base(ctrl) { }

        public BaseEntity Single(Expression<Func<Il, bool>> filter)
        {
            //Seçim işlemini selector burada devreye giricek fonksiyon parametresinde değil.
            //Burada Selector için Data Transfer Object Oluşturacağız DTO ..
            return BaseSingle(filter, x =>x);
        }

        public IEnumerable<BaseEntity> List(Expression<Func<Il, bool>> filter)
        {
            //BaseList IQueryable türünde döndüğü için bunu ekrana getirirken sıralama ve Listeleme yapıyoruz.
            //Burada ToList() döndürmemizin sebebi OrderBy ile filtre uygulamamız . Yani İlk Sıralama Sonra Listeleme işlemi
            //Gelen tiple çevirme yapacağımız tip farklı olması lazım DTO yapmamız gerekiyor
            return BaseList(filter, x =>x).OrderBy(x => x.Kod).ToList();
        }

        private object BaseList(Expression<Func<Okul, bool>> filter, Func<Il, Il> p)
        {
            throw new NotImplementedException();
        }

        public bool Insert(BaseEntity entity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseInsert(entity, x => x.Kod == entity.Kod);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseUpdate(oldEntity, currentEntity, x => x.Kod == currentEntity.Kod);
        }

        public bool Delete(BaseEntity entity)
        {

            return BaseDelete(entity, KartTuru.Il);

        }

        public string YeniKodVer()
        {
            //tür ve filter verdik -> Kod alanınını getir
            return BaseYeniKodVer(KartTuru.Il, x => x.Kod);
        }

    }
}
