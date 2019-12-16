using SolidOtomasyon.Takip.Model.Attributes;
using SolidOtomasyon.Takip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class Il:BaseEntityDurum
    {
        [Index("IX_Kod",IsUnique =true)]
        public override string Kod { get; set; }

        [Required,StringLength(50),ZorunluAlan("İl Adı","txtIlAdi")]
        public string IlAdi { get; set; }

        [StringLength(400)]
        public string Aciklama { get; set; }


    }
}
