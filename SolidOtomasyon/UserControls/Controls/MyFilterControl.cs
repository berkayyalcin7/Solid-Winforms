using DevExpress.XtraEditors;
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
    public class MyFilterControl:FilterControl,IStatusBarAciklama
    {

        public MyFilterControl()
        {
            // Gruplama yaparken icon' çıkmasını sağlar , default False'dir
            ShowGroupCommandsIcon = true;

        }

        public string StatusBarAciklama { get; set; } = "Filtre Metni Giriniz.";

    }
}
