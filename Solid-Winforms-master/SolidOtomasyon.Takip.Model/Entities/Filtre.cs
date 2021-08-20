using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class Filtre : BaseEntity
    {
        [Index("IX_Kod",IsUnique =false)]
        public override string Kod { get; set; }

        [Required,StringLength(100),ZorunluAlan("Filtre Adı","txtFiltreAdi")]
        public string FiltreAdi { get; set; }

        [Required,StringLength(1000), ZorunluAlan("Filtre Metni", "txtFiltreMetni")]
        public string FiltreMetni { get; set; }

        [Required]
        public KartTuru KartTuru { get; set; }
    }
}
