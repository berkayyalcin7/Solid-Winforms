using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class Ilce:BaseEntityDurum
    {
        //Burada İlçe Kodları aynı olabilir Farklı İllerin ilçe kodları eşleşebilir
        [Index("IX_Kod", IsUnique = false)]
        public override string Kod { get; set; }

        
        [Required,StringLength(50),ZorunluAlan("İlçe Adı", "txtIlceAdi")]
        public string IlceAdi { get; set; }

        [StringLength(400)]
        public string Aciklama { get; set; }

        //Il Entity'si ile ilişkili olması gerekiyor ->Migration işlemi sonrasında Il sonuna Id ekliyor varsa onu FK yapıyor.
        public long IlId { get; set; }
        public Il Il { get; set; }
    }
}
