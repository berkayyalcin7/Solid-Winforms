using SolidOtomasyon.Takip.Model.Entities.Base;

namespace SolidOtomasyon.Takip.Model.Entities
{
    public class Ilce:BaseEntityDurum
    {
        public string IlceAdi { get; set; }

        public string Aciklama { get; set; }

        //Il Entity'si ile ilişkili olması gerekiyor ->Migration işlemi sonrasında Il sonuna Id ekliyor varsa onu FK yapıyor.
        public long IlId { get; set; }
        public Il Il { get; set; }
    }
}
