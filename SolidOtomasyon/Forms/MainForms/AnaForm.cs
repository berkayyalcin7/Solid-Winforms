using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using SolidOtomasyon.Forms.OkulForms;

namespace SolidOtomasyon.Forms.MainForms
{
    public partial class AnaForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public AnaForm()
        {
            InitializeComponent();
            //Çalışması için Constructora ekliyoruz.
            EventsLoad();
        }

        //Event Load edildiğinde
        private void EventsLoad()
        {
            //Header menüdeki Ribborn Control de dolaşıyoruz
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarButtonItem btn:
                        btn.ItemClick += Butonlar_ItemClick;  
                        break;


                }

            }
        }

        //Event Fırlatmış olduk
        private void Butonlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.Item == btnOkulKartlari)
            {
                OkulListForm frm = new OkulListForm();
                //Aktif olan formun içinde aç
                frm.MdiParent = ActiveForm;
                frm.Show();
            }
        }
    }
}