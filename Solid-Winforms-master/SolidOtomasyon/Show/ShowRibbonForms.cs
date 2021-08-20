using DevExpress.XtraBars.Ribbon;
using System;

namespace SolidOtomasyon.Show
{
    public class ShowRibbonForms<TForm> where TForm:RibbonForm
    {
        public static void ShowForm(bool dialog,params object[] prm) //,params object[] pre)
        {
            //Instance Alıyoruz
            var frm = (TForm)Activator.CreateInstance(typeof(TForm), prm);
            //Dialog ise açılıp kapanabilir using içerisinde
            if (dialog)
            {
                using (frm)
                {
                    frm.ShowDialog();
                }

            }
            else
            {
                frm.Show();
            }
            
        }


    }
}
