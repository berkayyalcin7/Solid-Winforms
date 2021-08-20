using SolidOtomasyon.Takip.Data.OgrenciTakipMigration;
using SolidOtomasyon.Takip.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SolidOtomasyon.Takip.Data.Contexts
{


    //TConfiguration Hazýrlamak için -> OgrenciTakipMigration'a Config dosyasý oluþturuyoruz.
    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>
    {
     
        public OgrenciTakipContext()
        {
            // Herhangi Bir Select Ýþlemi yapýldýðýnda Sadece istenilen property'i getirir ...
            Configuration.LazyLoadingEnabled = false;
        }


        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        // Model Oluþtuðunda Table Ýsimlerini Çoðullaþtýrmamak için  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        // 2.OneToManyCascadeDeleteConvention  olarak Birden Çoða olan iliþkilerde herhangi bir Datayý sildiðimizde Ýliþkili olaný ' silme ..
        // 3. ManyToMany 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<Okul> Okul { get; set; }
        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Filtre> Filtre { get; set; }
        public DbSet<VeliBilgi> VeliBilgi { get; set; }
    
    }

   
}