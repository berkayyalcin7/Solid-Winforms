using System.ComponentModel;

namespace SolidOtomasyon.Takip.Common.Enums
{
    public enum RaporuKagidaSigdirma:byte
    {
        [Description("Sütünları Daraltarak Sığdır")]
        SutunlariDaraltarakSigdir = 1,
        [Description("Yazı Boyutunu Küçülterek Sığdır")]
        YaziBoyutunuKuculterekSigdir = 2,
        [Description("İşlem Yapma")]
        IslemYapma =3
    }
}
