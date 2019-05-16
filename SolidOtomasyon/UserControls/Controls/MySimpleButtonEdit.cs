using DevExpress.XtraEditors;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls
{
    [ToolboxItem(true)]
    public class MySimpleButtonEdit:SimpleButton,IStatusBarAciklama
    {

        public MySimpleButtonEdit()
        {
            Appearance.ForeColor = Color.Maroon;
                 
        }

        public string StatusBarAciklama { get;set; }
    }
}
