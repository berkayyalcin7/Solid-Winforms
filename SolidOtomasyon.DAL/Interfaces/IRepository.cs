using SolidOtomasyon.Takip.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolidOtomasyon.DAL.Interfaces
{
    // T Tipinde Bir Entity Göndereceğiz. -> T tipi sadece Class tipindeki değerler olacak . Örn : Entity'ler
    //IDisposable : Yani bu interface'i kullanan classlar İşlem bittiği  Anda hafızada yer kaplamıyor ... 

    public interface IRepository<T>:IDisposable where T:class
    {
        //Database ' E kaydetme işlemlerini UnitOfWork Patterni ile yapacağız ..


        // Örn : Insert(Okul yeniOkul)
        void Insert(T entity);
        // Toplu Veri Girişi -> 
        void Insert(IEnumerable<T> entities);
        //Normal Update İşlemi
        void Update(T entity);
        //Değişecek Alanların Listesini parametre olarak Göndereceğiz.
        void Update(T entity, IEnumerable<string> fields);
        //Toplu Update İşlemleri
        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        // Örn : Tek Bir Row için Select İşlemi -> Data Transfer Object tipinde dönecek
        // Geriye Değer Döndürebilmesi için T Tipinde Sorguyu Gönderiyoruz. ve bize geri bool tipinde değer göndericek
        // TResult İse Find Fonksiyonunun verdiğimiz Tipe dönüşmesini sağlar.
        // Select İfadesi için Bir Expression selector tanımlaması yaptık
        TResult Find<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector);

        // Birden Fazla Kayıt Seçmek İçin Fonksiyon -> IQueryable türünde -> T tipini barındıran filtre Select * from Okul
        // IQueryable bize String Olarak Bir Sorgu döndürür.
        // Select İfadesi için Bir Expression selector tanımlaması yaptık
        IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector);

        int Count(Expression<Func<T, bool>> filter=null);


        string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null);


    }
}
