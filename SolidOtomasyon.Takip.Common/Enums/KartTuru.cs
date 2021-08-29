using System.ComponentModel;

namespace SolidOtomasyon.Takip.Common.Enums
{
    //Bazen Enum değerlerini Veritabanına kaydedeceğiz -> enum'ı int değil byte olarak kaydediyoruz. Daha Az yer kaplaması için
    public enum KartTuru : byte
    {
        [Description("Okul Kartı")]
        Okul = 1,
        [Description("İl Kartı")]
        Il = 2,
        [Description("İlçe Kartı")]
        Ilce = 3,
        [Description("Filtre Kartı")]
        Filtre = 4,
        [Description("Veli Bilgi Kartı")]
        VeliBilgi = 5,
        [Description("İptal Nedeni Kartı")]
        IptalNedeni = 6,


    }
}
