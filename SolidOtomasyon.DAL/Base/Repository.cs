using SolidOtomasyon.DAL.Interfaces;
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
            if(context == null) { return; }
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
                entry.Property(field).IsModified=true;
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
