using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Forms.MainForms;
using System;
using System.Windows.Forms;

namespace SolidOtomasyon.Takip.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnaForm());
        }
    }
}
