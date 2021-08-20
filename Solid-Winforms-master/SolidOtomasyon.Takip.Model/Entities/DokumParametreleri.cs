using SolidOtomasyon.Takip.Common.Enums;
using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class DokumParametreleri:IBaseEntity
    {
        public string RaporBaslik { get; set; }

        public EvetHayir BaslikEkle { get; set; }

        public RaporuKagidaSigdirma RaporuKagidaSigdir { get; set; }

        public YazdirmaYonu YazdirmaYonu { get; set; }

        public EvetHayir YatayCizgileriGoster { get; set; }

        public EvetHayir DikeyCizgileriGoster { get; set; }

        public EvetHayir SutunBasliklariniGoster { get; set; }

        public string YaziciAdi { get; set; }

        public int YazdirilacakAdet { get; set; }

        public DokumSekli DokumSekli { get; set; }
    }
}
