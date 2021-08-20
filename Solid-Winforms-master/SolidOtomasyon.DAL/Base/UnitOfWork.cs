using SolidOtomasyon.DAL.Interfaces;
using SolidOtomasyon.Takip.Common.Message;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.DAL.Base
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        #region Variables
        private readonly DbContext _context;
        #endregion


        public UnitOfWork(DbContext context)
        {
            if (context == null) return;
            _context = context;

        }

        //Context'imizi göndereceğiz instance alınacak
        public IRepository<T> Rep => new Repository<T>(_context);

        //Kayıt İşlemleri Hata Mesajları
        public bool Save()
        {
            //Hata Durumunda
            try
            {
                _context.SaveChanges();
            }
            //Genel olarak Db Update Hatalarını yakalayacağız
            catch (DbUpdateException ex)
            {
                //Null ise herhangi bir işlem yapma -> SqlException'a cast Ediyoruz 
                var sqlEx = (SqlException)ex.InnerException?.InnerException;
                if (sqlEx == null)
                {
                    Messages.HataMesaji(ex.Message);
                    return false;
                }
                //Diğer Hatalar 
                switch (sqlEx.Number)
                {
                    //208 : Tablo Db ' de bulunamadı
                    case 208:
                        Messages.HataMesaji("İşlem Yapmak istediğiniz Tablo Veritabanında Bulunamadı");
                        break;
                    // Kullanılan bi Entity Başka yerde kullanılıyorsa Silinemez 
                    case 547:
                        Messages.HataMesaji("Seçilen Kartın İşlem Görülmüş Hareketleri Var Kart Silinemez");
                        break;
                    // Aynı Değerin Üretilmesi sonucu verilen Hata
                    case 2601:
                    case 2627:
                        Messages.HataMesaji("Girmiş olduğunuz ID Daha Önce Kullanılmıştır");
                        break;

                    case 4060:
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Veritabanı Sunucuda Bulunamadı");
                        break;

                    case 18456:
                        Messages.HataMesaji("Server'a Bağlanılmak İstenilen Kullanıcı Adı veya Şifre Hatalıdır");
                        break;

                    default:
                        Messages.HataMesaji(sqlEx.Message);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
                return false;
            }

            return true;
        }

        #region Dispose
        private bool _disposedValue = false; // To detect redundant calls

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

            GC.SuppressFinalize(this);
        }
        #endregion




    }


}
