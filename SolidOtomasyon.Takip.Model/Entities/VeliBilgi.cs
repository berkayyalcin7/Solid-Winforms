using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class VeliBilgi:BaseEntityDurum
    {
        [Index("IX_Kod",IsUnique =true)]
        public override string Kod { get; set; }

        [Required,StringLength(100),ZorunluAlan("Bilgi Adı Zorunludur","txtBilgiAdi")]
        public string BilgiAdi { get; set; }

        [StringLength(400)]
        public string Aciklama { get; set; }


    }
}
