using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SolidOtomasyon.Takip.Data.Contexts
{
    // 1. Context'imiz , 2. Mig ayarları , TConfig Kullanab ilmek İçin T ' ile instance New almak gerekiyor
    public class BaseDbContext<TContext,TConfiguration>:DbContext where TContext:DbContext where TConfiguration:DbMigrationsConfiguration<TContext>,new()
    {
        // Context'imizin Name ' ini typeof .Name ile alabiliyouz. -> Örneğin : OgrenciContext
        private static string _nameOrConnectionString = typeof(TContext).Name;

        //Null olamaz -> Kendi ConnectionString'imizi göndereceğiz
        public BaseDbContext() : base(_nameOrConnectionString) { }

        //Parametre olarak connectionString'in kendisini göndereceğiz -> Her değiştinde yüklenicek
        public BaseDbContext(string connectionString):base(connectionString)
        {
            //Migration İçin -> Db ye bağlanıcak Karşılaştırma yapacak -> Değişiklik durumunda güncelleme yap
            //TContext -> OgrenciContext TConfig-> Mig ayarı
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>());

            _nameOrConnectionString = connectionString;

        }


    }
}
