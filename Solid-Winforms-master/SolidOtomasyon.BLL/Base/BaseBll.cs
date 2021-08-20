using SolidOtomasyon.BLL.Functions;
using SolidOtomasyon.BLL.Interfaces;
using SolidOtomasyon.DAL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Functions;
using SolidOtomasyon.Takip.Common.Message;
using SolidOtomasyon.Takip.Model.Attributes;
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
        #region Değişkenler
        private readonly Control _ctrl;

        //Repository'mize UnitOfWork üzerinden erişeceğiz
        private IUnitOfWork<T> _uow;

        #endregion

        private bool Validation(IslemTuru islemTuru, BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            var errorControl = GetValidationErrorControl();

            if (errorControl == null) return true;
            //Hatalı olan kontrole focuslan
            _ctrl.Controls[errorControl].Focus();
            return false;

            string GetValidationErrorControl()
            {

                string VarOlanKod()
                {
                    foreach (var property in typeof(T).GetPropertyAttributeFromType<Kod>())
                    {
                        //Kod alanını arayacağız
                        if (property.Attribute == null)
                            continue;


                        if ((islemTuru == IslemTuru.EntityInsert || oldEntity.Kod == currentEntity.Kod) && islemTuru == IslemTuru.EntityUpdate)
                        {
                            continue;
                        }
                        //Veritabanına Kodu gönderdikten sonra olup olmadığı kontrolü eğer yok ise continue ile işleme devam et
                        if (_uow.Rep.Count(filter) < 1)
                        {
                            continue;
                        }
                        // Örnek-> Kod
                        Messages.VarOlanKodKayitHataMesaji(property.Attribute.Description);
                        //Hatanın oluşmuş olduğu control
                        return property.Attribute.ControlName;


                    }
                    return null;
                }

                string HataliGiris()
                {
                    foreach (var property in typeof(T).GetPropertyAttributeFromType<ZorunluAlan>())
                    {
                        //Kod alanını arayacağız
                        if (property.Attribute == null)
                            continue;

                        var value = property.Property.GetValue(currentEntity);

                        //long 0 geliyorsa null olarak ata
                        if (property.Property.PropertyType == typeof(long))
                        {
                            if ((long)value == 0)
                            {
                                value = null;
                            }
                        }

                        //Yukarıdan gelen value null değil ise stringe çevir ve kontrol et 
                        if (!string.IsNullOrEmpty(value?.ToString()))
                        {
                            continue;
                        }

                        Messages.HataliVeriMesaji(property.Attribute.Description);
                        //Hatanın oluşmuş olduğu control
                        return property.Attribute.ControlName;
                    }

                    return null;
                }
                // İlk olarak Hatali Girişi kontrol ediyoruz daha sonra VarOlanKod Kontrolü yapıyoruz 
                return HataliGiris() ?? VarOlanKod();
            }


        }


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

            if (!Validation(IslemTuru.EntityInsert, null, entity, filter))
            {
                return false;
            }

            //Repository'e OgrenciTakipContext'de tanımlanmış entitylerden birisini atıyoruz T
            _uow.Rep.Insert(entity.EntityConvert<T>());

            return _uow.Save();
        }

        //Entity'nin eski ve yeni hali olarak 2 entity alacak
        protected bool BaseUpdate(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);

            if (!Validation(IslemTuru.EntityUpdate, oldEntity, currentEntity, filter))
            {
                return false;

            }
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

        // Where bazı durumlarda kullanılacak expresion Örn : İl KartlarındDA
        protected string BaseYeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);

            return _uow.Rep.YeniKodVer(kartTuru, filter, where);
        }


        public void Dispose()
        {
            //Null değil ise Dispose ET Bütün BLL ' i dispose etmiyoruz.
            _ctrl?.Dispose();
            _uow?.Dispose();
        }



    }
}
