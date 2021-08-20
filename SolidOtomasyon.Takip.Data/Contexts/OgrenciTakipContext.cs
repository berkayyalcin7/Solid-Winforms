using SolidOtomasyon.Takip.Data.OgrenciTakipMigration;
using SolidOtomasyon.Takip.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SolidOtomasyon.Takip.Data.Contexts
{


    //TConfiguration Haz�rlamak i�in -> OgrenciTakipMigration'a Config dosyas� olu�turuyoruz.
    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>
    {
     
        public OgrenciTakipContext()
        {
            // Herhangi Bir Select ��lemi yap�ld���nda Sadece istenilen property'i getirir ...
            Configuration.LazyLoadingEnabled = false;
        }


        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        // Model Olu�tu�unda Table �simlerini �o�ulla�t�rmamak i�in  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        // 2.OneToManyCascadeDeleteConvention  olarak Birden �o�a olan ili�kilerde herhangi bir Datay� sildi�imizde �li�kili olan� ' silme ..
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