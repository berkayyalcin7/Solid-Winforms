using DevExpress.XtraEditors;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    //Buton özelliği olmayanlar IStatusBarAciklama'dan implemente oluyor ...
    public class MyCheckEdit : CheckEdit,IStatusBarAciklama
    {

        public MyCheckEdit()
        {
            Properties.Appearance.BackColor =Color.Transparent;
        }


        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarAciklama { get; set; }
    }
}
