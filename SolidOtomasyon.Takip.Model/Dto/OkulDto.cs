using SolidOtomasyon.Takip.Model.Entities;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Model.Dto
{
    //Dosya ismi OkulDto kalıcak class değişecek
    //OkulS : Single'ı ifade edicek OkulL : List'i ifade edecektir.
    //Burada 'ki 2 property vt ' ye eklenmeyecek bunun için NotMapped Kullancağız
    [NotMapped]
    public class OkulS : Okul
    {
        //DTO ' lar ilişkilendirilmiş tablolardaki diğer propertyleri çekmemize yarayacak.

        public string IlAdi { get; set; }
        public string IlceAdi { get; set; }

    }

    public class OkulL : BaseEntity
    {
        //DTO ' lar ilişkilendirilmiş tablolardaki diğer propertyleri çekmemize yarayacak.
        //Durum'unu göstermeye gerek olmadığı için o property' eklenmedi
        public string OkulAdi { get; set; }
        public string IlAdi { get; set; }
        public string IlceAdi { get; set; }
        public string Aciklama { get; set; }


    }
}

