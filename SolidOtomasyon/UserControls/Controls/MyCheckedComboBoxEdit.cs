using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SolidOtomasyon.Interfaces;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyCheckedComboBoxEdit : CheckedComboBoxEdit,IStatusBarKisaYol
    {

        public MyCheckedComboBoxEdit()
        {
            Properties.AppearanceFocused.BackColor = Color.LightCyan;

            //Combobox Text Girişini Engelliyoruz .. Default Değerdir
            //Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

        }

        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarKisaYol { get; set; } = "F4 : ";
        public string StatusBarKisaYolAciklama { get; set; }
        public string StatusBarAciklama { get; set; }
    }
}
