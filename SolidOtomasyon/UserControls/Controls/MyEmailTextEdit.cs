using DevExpress.XtraEditors.Mask;
using SolidOtomasyon.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyEmailTextEdit : MyTextEdit
    {
        public MyEmailTextEdit()
        {
            //Email : RegEx tipinde
            Properties.Mask.MaskType = MaskType.RegEx;

            // E-mail maskesi
            Properties.Mask.EditMask = @"((([0-9a-zA-Z_%-])+[.])+|([0-9a-zA-Z_%-])+)+@((([0-9a-zA-Z_-])+[.])+|([0-9a-zA-Z_-])+)+";


            //Default da seçilebilir ...
            Properties.Mask.AutoComplete = AutoCompleteType.Strong;

            StatusBarAciklama = "E-mail Adresi Giriniz. ";


        }

    }
}
