using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Data.Contexts;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.Base
{
    public class BaseGenelBll<T>:BaseBll<T,OgrenciTakipContext> where T:BaseEntity
    {

        #region Değişkenler
        private KartTuru _kartTuru; 
        #endregion

        public BaseGenelBll(KartTuru kartTuru)
        {
            //KartTuru göndermemiz gerekiyro yoksa Silerken hata alacağız
            _kartTuru = kartTuru;
        }

        public BaseGenelBll(Control ctrl,KartTuru kartTuru):base(ctrl)
        {
            _kartTuru = kartTuru;
        }

        //Override edilip kullanılacak virtual diyoruz ...
        public virtual BaseEntity Single(Expression<Func<T, bool>> filter)
        {
            //Seçim işlemini selector burada devreye giricek fonksiyon parametresinde değil.
            //Burada Selector için Data Transfer Object Oluşturacağız DTO ..
            return BaseSingle(filter, x => x );
        }

        public virtual IEnumerable<BaseEntity> List(Expression<Func<T, bool>> filter)
        {
            //BaseList IQueryable türünde döndüğü için bunu ekrana getirirken sıralama ve Listeleme yapıyoruz.
            //Burada ToList() döndürmemizin sebebi OrderBy ile filtre uygulamamız . Yani İlk Sıralama Sonra Listeleme işlemi
            return BaseList(filter, x => x);
        }

        public bool Insert(BaseEntity entity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseInsert(entity, x => x.Kod == entity.Kod);
        }

        public bool Insert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseInsert(entity, filter);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseUpdate(oldEntity, currentEntity, x => x.Kod == currentEntity.Kod);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            //Validation İşlemleri sırasında bu kod açıklanacak BaseBll'de yapılacak olan
            return BaseUpdate(oldEntity, currentEntity, filter);
        }

        public bool Delete(BaseEntity entity)
        {

            return BaseDelete(entity, _kartTuru);

        }

        public string YeniKodVer()
        {
            //tür ve filter verdik -> Kod alanınını getir
            return BaseYeniKodVer(_kartTuru, x => x.Kod);
        }

        public string YeniKodVer(Expression<Func<T, bool>> filter)
        {
            //tür ve filter verdik -> Kod alanınını getir
            return BaseYeniKodVer(_kartTuru, x => x.Kod, filter);
        }

    }
}
