using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyTelefonTextEdit:MyTextEdit
    {
        public MyTelefonTextEdit()
        {
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            Properties.Mask.MaskType = MaskType.Regular;

            Properties.Mask.EditMask = @"(\d?\d?\d?) \d?\d?\d? \d?\d?  \d?\d?";

            Properties.Mask.AutoComplete = AutoCompleteType.None;

            StatusBarAciklama = "Telefon No Giriniz. ";
        }


    }
}
