using DevExpress.Utils;
using DevExpress.XtraEditors;
using SolidOtomasyon.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyCalcEdit : CalcEdit,IStatusBarKisaYol
    {


        public MyCalcEdit()
        {
            // Ayarlarımız

            Properties.AppearanceFocused.BackColor = Color.Bisque;

            // Null değer alamasın
            Properties.AllowNullInput = DefaultBoolean.False;

            // n -> number n2 , virgülden sonra 2 hane
            Properties.EditMask = "n2";

        }

        public override bool EnterMoveNextControl { get; set; } = true;


        public string StatusBarKisaYol { get; set; } = "F4 : ";

        public string StatusBarKisaYolAciklama { get; set; } = "Hesap Makinesi";

        public string StatusBarAciklama { get; set; }
    }
}
