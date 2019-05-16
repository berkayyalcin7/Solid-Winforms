using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Controls
{
    //Resim Seçme işlemi button tarzı olduğu için IStatusBarKisaYol
    [ToolboxItem(true)]
    public class MyPictureEdit:PictureEdit,IStatusBarKisaYol
    {
        public MyPictureEdit()
        {
            Properties.AppearanceFocused.BackColor =Color.LightCyan;

            //Resim Olmadığında ekranda yazan yazının rengi

            Properties.Appearance.ForeColor = Color.Maroon;

            //Resim silindiğinde ekrandaki yazı

            Properties.NullText = "Resim Yok";

            //Resim Size modu : Strech ile yayacak.

            Properties.SizeMode = PictureSizeMode.Stretch;

            //Resmin üzerine sağ tıklandığında menüyü gizleme

            Properties.ShowMenu = false;

            
        }

        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarKisaYol { get; set; } = "F4 :";

        public string StatusBarKisaYolAciklama { get; set; }

        public string StatusBarAciklama { get; set; }

    }
}
