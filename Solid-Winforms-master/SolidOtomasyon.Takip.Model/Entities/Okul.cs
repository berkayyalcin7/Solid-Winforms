using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities
{
    //Aynı Zamanda BaseEntity'deki Özellikleri de almış oluyor
    public class Okul:BaseEntityDurum
    {
        //Indexle'me entityframework ile geliyor
        [Index("IX_Kod",IsUnique =true)]
        public override string Kod { get; set; }

        [Required,StringLength(50),ZorunluAlan("Okul Adı", "txtOkulAdi")]
        public string OkulAdi { get; set; }
        
        //İl ve İlçeyi ID üzerinden  yakalayacağız -> String yapmama sebebimiz değerleri Il ve İlçe tablosundan alacak olmamız
        // Burada txtIl ve txtIlce buton türünde olduğu için böyle isimlendirilmişti
        [ZorunluAlan("İl Adı","txtIl")]
        public long IlId { get; set; }
        [ZorunluAlan("İlçe Adı", "txtIlce")]
        public long IlceId { get; set; }

        [StringLength(400)]
        public string Aciklama { get; set; }

        //Relation -> Kuruldu .
        public Il Il { get; set; }

        //Bu Kullanım yöntemi Manuel olarak Hangi Property'i FK yapacağımızı belirtiyoruz.
        [ForeignKey("IlceId")]
        public Ilce Ilce { get; set; }


    }
}
