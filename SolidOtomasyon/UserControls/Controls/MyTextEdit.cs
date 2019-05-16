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


    // DevExpress . XtraEditors
    [ToolboxItem(true)]
    public class MyTextEdit : TextEdit,IStatusBarAciklama
    {


        public MyTextEdit()
        {
            //Ayarlarımız -> Arka  PLan Renk Ayarı

            Properties.AppearanceFocused.BackColor = Color.LightCyan;

            Properties.MaxLength = 45;

          
        }

        //Enter Bastığında diğer controle geçme
        public override bool EnterMoveNextControl { get; set; } = true;


        //Implement Interface
        public string StatusBarAciklama { get; set; }
    }
}
