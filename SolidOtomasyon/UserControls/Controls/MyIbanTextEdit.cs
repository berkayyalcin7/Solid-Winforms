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
    public class MyIbanTextEdit:MyTextEdit
    {


        public MyIbanTextEdit()
        {
            Properties.Mask.MaskType = MaskType.Regular;

            //Toplam alan TR ile birlikte 26 
            Properties.Mask.EditMask = @"TR\d?\d? \d?\d?\d?\d? \d?\d?\d?\d?  \d?\d?\d?\d? \d?\d?\d?\d? \d?\d?\d?\d? \d?\d?";
            //Oto tanımlama kapalı
            Properties.Mask.AutoComplete = AutoCompleteType.None;

            StatusBarAciklama = "IBAN No Giriniz .";



        }



    }
}
