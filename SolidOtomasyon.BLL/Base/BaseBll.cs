using SolidOtomasyon.BLL.Functions;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.DAL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Functions;
using SolidOtomasyon.Takip.Common.Message;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SolidOtomasyon.BLL.Base
{
    //Entity ve Context'imiz olacak
    public class BaseBll<T, TContext> : IBaseBll where T : BaseEntity where TContext : DbContext
    {
        private readonly Control _ctrl;

        //Repository'mize UnitOfWork üzerinden erişeceğiz
        private IUnitOfWork<T> _uow;


        //Sadece implemente edeen classların erişimi için protected tanımlıyoruz
        protected BaseBll() { }


        protected BaseBll(Control ctrl)
        {

            _ctrl = ctrl;

        }

        //Tek bir değer getiren BaseSingle metodu
        protected TResult BaseSingle<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            //UnitOfWork Create etmiş olduk. -> Instance alınmış şekilde
            //Buna ihtayaç duymamızın sebebi ConnectionString'imizin değişmesi durumunda En güncel şekilde instance ' ı alır.
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            //UnitOfWork'den Rep 'e ordanda Find fonksiyonuna -> filtre ve selector'umuzu atıyoruz
            //Bu şekilde çalıştırırsak Instance olmadığı için hata vericektir. (Burdan Devam)
            return _uow.Rep.Find(filter, selector);
        }

        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            //Select İşlemi ile filtremizi ve selector (sorgumuzu veriyoruz.)
            return _uow.Rep.Select(filter, selector);
        }

        //Entity'ler için Data Transfer Object Oluşturacağız ve Convert Edeceğiz
        protected bool BaseInsert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            //Validation İşlemleri yapılacak ...

            //Repository'e OgrenciTakipContext'de tanımlanmış entitylerden birisini atıyoruz T
            _uow.Rep.Insert(entity.EntityConvert<T>());

            return _uow.Save();
        }

        //Entity'nin eski ve yeni hali olarak 2 entity alacak
        protected bool BaseUpdate(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);

            //Bunun için ayrı bir Fonksiyon tanımlayacağız.
            var degisenAlanlar = oldEntity.DegisenAlanlariGetir(currentEntity);

            //Değişen alan yoksa işlemi burda bitir true döndür.
            if (degisenAlanlar.Count == 0) return true;

            //EntityConvert yaptık.
            _uow.Rep.Update(currentEntity.EntityConvert<T>(), degisenAlanlar);

            return _uow.Save();
        }

        //Hangi Kartın silindiğini yakalamak için burada Bir Enum Tanımlayacağız .. Örn : Seçilen "Okul Kartı" silinecektir.

        //Mesaj ver kısmı silinicek yerlerde onay için çıkan kutucuk olacak , Bazı yerlerde false olarak kullanacağız !!
        protected bool BaseDelete(BaseEntity entity, KartTuru kartTuru, bool mesajVer = true)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);

            if (mesajVer)
            {
                //Messages Kısmı ile bu bölümü düzenleyeceğiz ... Enum'ların descriptionunu okuyacak function
                if (Messages.SilMesaj(kartTuru.ToName()) != DialogResult.Yes) return false;

            }
            _uow.Rep.Delete(entity.EntityConvert<T>());

            return _uow.Save();
        }

        public void Dispose()
        {
            //Null değil ise Dispose ET Bütün BLL ' i dispose etmiyoruz.
            _ctrl?.Dispose();
            _uow?.Dispose();
        }


    }
}
