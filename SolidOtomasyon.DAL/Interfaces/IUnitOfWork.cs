using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.DAL.Interfaces
{
    //Bize sağlamış olduğu avantaj : Tek Seferde Db'ye göndermeye yarıyor (Yapılan İşlemleri)
    //Repository'de Save İşlemi Yapılmıyor -> Toplu Olarak Hepsini Repoya göndericeğiz Save İşlemini UnitofWork Yapacak.
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        //Bu Şekilde UnitOfWork'den IRepository'ye ulaşabiliyoruz. Get ile alıyoruz
        IRepository<T> Rep { get; }
        //Save Fonksiyonu geriye değer döndürecek.
        bool Save();

    }
}
