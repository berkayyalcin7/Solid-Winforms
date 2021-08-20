using SolidOtomasyon.Takip.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Data.OgrenciTakipMigration
{
    //Config Ayarlarımız Burada Yapılacak -> Hangi context olduğunu belirtiyoruz.
    public class Configuration:DbMigrationsConfiguration<OgrenciTakipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Eklenmiş verinin tipi değiştiğinde bi veri kaybına izin verir.
            AutomaticMigrationDataLossAllowed = true;
        }




    }
}
