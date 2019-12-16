using SolidOtomasyon.DAL.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Common.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SolidOtomasyon.DAL.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Variables
        //Bu Yerel değişken olduğu için _ ile başladık 
        //-> ReadOnly alana Ctor 'de değer atayabiliriz . Ama Başka yerde atama yapamayız.
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;
        #endregion

        //Çeşitli İşlemler İçin Constructor
        public Repository(DbContext context)
        {
            if (context == null) { return; }
            _context = context;
            //DbSet'imizi de atmış olduk.
            _dbSet = _context.Set<T>();

        }

        #region CRUD İŞLEMLERİ
        public void Insert(T entity)
        {
            // Yeni Bir Kayıt olarak ekleyecek
            _context.Entry(entity).State = EntityState.Added;

        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(T entity, IEnumerable<string> fields)
        {
            // _dbSet te güncelleme yapacağız
            _dbSet.Attach(entity);

            var entry = _context.Entry(entity);

            foreach (var field in fields)
            {
                //Hangisinde değişiklik varsa Modifie yap
                entry.Property(field).IsModified = true;
            }

        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }
        #endregion

        public TResult Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            //Null ise First or Defalut -> Değil ise Where ile filtreleyip değeri getiriyoruz.
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();

        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {

            //SQL Kodu Geri Döndürecek -> Liste veya Değer değil
            return filter == null ? _dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);

        }


        //bURDAN dEVAM
        public string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            string Kod()
            {
                string kod = null;
                //Kart Türü Adına Ulaşıyoruz
                var kodDizi = kartTuru.ToName().Split(' ');

                for (int i = 0; i < kodDizi.Length - 1; i++)
                {
                    //indexdeki kelimeyi al -> Okul Kartı ise Okul kısmını alacaktır.
                    kod += kodDizi[i];

                    if (i + 1 < kodDizi.Length - 1)
                        //Okul kartı için 
                        kod += " ";
                }

                return kod += "-0001";
            }


            string YeniKodVer(string kod) // Okul-0002 geldi ise
            {
                var sayisalDegerler = "";
                foreach (var karakter in kod)
                {
                    if (char.IsDigit(karakter))
                    {
                        sayisalDegerler += karakter;
                    }
                    else
                        sayisalDegerler = "";
                }

                var artisSonrasiDeger = (int.Parse(sayisalDegerler) + 1).ToString(); // 0049 ise // 50 olacak
                var fark = kod.Length - artisSonrasiDeger.Length; 
                if (fark < 0)
                    fark = 0;

                //0 ' DAN FARKA KADAR OLAN KARAKTERLERİ ALIYORUZ.
                var yeniDeger = kod.Substring(0, fark); 
                yeniDeger += artisSonrasiDeger; // Okul-0050

                return yeniDeger;
            }
            //Arttırılmış olarak kodu getirmiş olacak
            var maxKod = where == null ? _dbSet.Max(filter) : _dbSet.Where(where).Max(filter);

            //max kod null ise Kod() değilse YeniKodVer çalışacak
            return maxKod == null ? Kod() : YeniKodVer(maxKod);
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {

            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }


        #region Dispose İşlemleri

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();

                }



                _disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            //Garbage Collector ile hafızadan atıyoruz .
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
