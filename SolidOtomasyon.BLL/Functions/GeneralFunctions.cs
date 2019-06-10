using SolidOtomasyon.DAL.Base;
using SolidOtomasyon.DAL.Interfaces;
using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace SolidOtomasyon.BLL.Functions
{
    public static class GeneralFunctions
    {

        //Extension metot olarak tanımlanması için static ve this keywordu kullanılması gerekiyor
        public static IList<string> DegisenAlanlariGetir<T>(this T oldEntity, T currentEntity)
        {
            //Interface'lerin instance'ı alınamaz !!!
            IList<string> alanlar = new List<string>();

            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                // ICollection Tipinde bi Property' sahip ise örnek ICollection<Ilce> Ilce tipinde ise işlem yapmadan devam et
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                // Gelen değer Null ise String.Empty yap -> Karşılaştırabilmesi için Emptye dönüştürdük.
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;

                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                //Örnek PRopertyler Byte Dizisi şeklindeyse -> Resim Dosyası
                if(prop.PropertyType == typeof(byte[]))
                {
                    if(string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        //Eğer oldValue null veya empty ise byte oluştur ve Default 0 ata
                        oldValue = new byte[] { 0 };
                    }

                    if (string.IsNullOrEmpty(currentValue.ToString()))
                    {
                        //Eğer currentValue null veya empty ise byte oluştur ve Default 0 ata
                        currentValue = new byte[] { 0 };
                    }
                    //Byte'a cast edip Uzunluğunu alıyoruz.
                    if(((byte[])oldValue).Length != ((byte[])currentValue).Length)
                    {
                        alanlar.Add(prop.Name);
                    }

                }
                //Eşit değil ise
                else if(!currentValue.Equals(oldValue))
                {
                    alanlar.Add(prop.Name);
                }

            }

            return alanlar;

        }

        //Genel Fonksiyonlar Bölümü
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SolidBaglanti"].ConnectionString;
        }

        //TContext'i ne olduğunu tanımlamamız lazım -> new() ile instance alınabildiğini ve geri gönderebiliriz.
        private static TContext CreateContext<TContext>() where TContext : DbContext
        {
            //Activator.CreateInstance ile new'leyebiliriz -> TContext'e Cast Etmez isek hata vericektir.
            // 1. parametre DbContext olduğu , 2. Connection Stringi gönderiyoruz.
            return (TContext)Activator.CreateInstance(typeof(TContext), GetConnectionString());
        }

        //Referans olarak Interfacei aldık -> T : Model katmanındaki genel Entity olan IBaseEntity interface'i
        public static void CreateUnitOfWork<T, TContext>(ref IUnitOfWork<T> uow) where T : class, IBaseEntity where TContext : DbContext
        {
            //uow'un son haline ulaşacağımız için başta var ise dispose ediceğiz
            // ? null değil ise
            uow?.Dispose();
            //Burada Yeni Bir UnitofWork Oluşturmuş olduk.
            uow = new UnitOfWork<T>(CreateContext<TContext>());
        }

    }
}
