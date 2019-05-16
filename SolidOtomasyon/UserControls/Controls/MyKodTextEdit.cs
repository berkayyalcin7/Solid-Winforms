using DevExpress.Utils;
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
    public class MyKodTextEdit:MyTextEdit
    {
        public MyKodTextEdit()
        {
            // Sabit bir back color
            Properties.Appearance.BackColor = Color.PaleGoldenrod;

            //Texti ortalama
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            //20 karakterle sınırlandırma
            Properties.MaxLength = 20;

            StatusBarAciklama = "Kod Giriniz.";
        }
    }
}
