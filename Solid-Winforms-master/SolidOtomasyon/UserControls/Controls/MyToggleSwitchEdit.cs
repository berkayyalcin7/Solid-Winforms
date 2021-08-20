using DevExpress.Utils;
using DevExpress.XtraEditors;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyToggleSwitchEdit:ToggleSwitch,IStatusBarAciklama
    {
        public MyToggleSwitchEdit()
        {
            //İsim Atıyoruz -> Aktif veya Pasife göre tglDurum
            Name = "tglDurum";
            //Kapalı olması durumunda yazılacak
            Properties.OffText = "Pasif";

            Properties.OnText = "Aktif";

            //Yükseklik standart kalacak.
            Properties.AutoHeight = false;
            //Data Layout kullandığımız zaman bu controlü o hücreye göre oto ayarlayacak.
            Properties.AutoWidth = true;

            //Solunda gözükmesi için Far
            Properties.GlyphAlignment = HorzAlignment.Far;

            Properties.Appearance.ForeColor = Color.Maroon;
        }

        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarAciklama { get; set; } = "Kartın Kullanım Durumunu Seçiniz. ";
    }
}
