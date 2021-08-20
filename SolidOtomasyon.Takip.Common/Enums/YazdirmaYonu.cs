using System.ComponentModel;

namespace SolidOtomasyon.Takip.Common.Enums
{
    public enum YazdirmaYonu:byte
    {
        [Description("Dikey")]
        Dikey = 1,
        [Description("Yatay")]
        Yatay = 2,
        [Description("Otomatik")]
        Otomatik =3
    }
}
