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
using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.BLL.General;

namespace SolidOtomasyon.Forms.OkulForms
{
    //BaseKart Bize daha az kod yazma kolaylığı sağlayacak.
    public partial class OkulListForm : BaseListForm
    {
        public OkulListForm()
        {
            InitializeComponent();

            //Geçici Olarak Vt'nin oluşması için tetikliyoruz.
            OkulBll bll = new OkulBll();

            //Gerekli Model Referansını ekliyoruz
            grid.DataSource = bll.List(null);

        }

    }
}