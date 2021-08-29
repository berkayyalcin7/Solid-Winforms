using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class IptalNedeni:BaseEntityDurum
    {
        [Index("IX_Kod", IsUnique = true)]
        public override string Kod { get; set; }

        [Required, StringLength(100), ZorunluAlan("İptal Nedeni Adı Zorunludur", "txtIptalNedeniAdi")]
        public string IptalNedeniAdi { get; set; }

        [StringLength(400)]
        public string Aciklama { get; set; }
    }
}
