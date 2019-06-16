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
using DevExpress.XtraBars.Ribbon;
using SolidOtomasyon.Takip.Common.Enums;

namespace SolidOtomasyon.Forms.BaseForms
{
    public partial class BaseEditForm :RibbonForm
    {
        //Protected : kalıtım alınan sınıflar kullanabilir.
        protected internal IslemTuru IslemTuru;

        //id Alanlarımızı BAse'de yapacağız
        protected internal long Id;

        protected internal bool RefreshYapilacak;


        public BaseEditForm()
        {
            InitializeComponent();
        }

        //Override edeceğimiz zaman internal kısmını virtual yapacağız
        protected internal void Yukle()
        {

        }



   
    }
}