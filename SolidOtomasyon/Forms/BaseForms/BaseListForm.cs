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
using DevExpress.XtraBars.Ribbon;

namespace SolidOtomasyon.Forms.BaseForms
{
    public partial class BaseListForm : RibbonForm
    {
        public BaseListForm()
        {
            InitializeComponent();
            EventsLoad();
        }

        private void EventsLoad()
        {
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    //BarButton ve BarSubItem 'ın ortak olduğu implemente class'ı bulup onu dahil ediyruz.
                    case BarItem button:
                        button.ItemClick += Button_ItemClick;
                        break;
                }


            }
        }

        //Generic oluşturacağız ve Hangi Formu göstereceğini belirleyeceğiz.
        //ID alanına göre çekeceğiz
        private void ShowEditForm(long id)
        {
            //Hangi formu açacağını bilmesi lazım -> Intrerfaceden ulaşacağız.
            var result = 
        }

        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == btnGonder)
            {
                //Link'i açmamız lazım SubItem olduğunu bildiğimiz için cast ediyoruz
                var link = (BarSubItemLink)e.Item.Links[0];

                link.Focus();
                link.OpenMenu();
                //Açıldığında ilkine focuslanacak.
                link.Item.ItemLinks[0].Focus();

            }
            else if(e.Item ==btnStandartExcellDosyasi)
            {

            }
            else if(e.Item==btnFormatliExcellDosyasi)
            {

            }
            else if(e.Item==btnFormatsizExcellDosyasi)
            {

            }
            else if(e.Item==btnWordDosyasi)
            {

            }
            else if (e.Item == btnPdfDosyasi)
            {

            }
            else if (e.Item == btnMetinDosyasi)
            {

            }
            else if (e.Item==btnYeni)
            {
                // Yetki kontrolü olacka Yeni dosya olabilmek için

                //Yeni Form burada açılcak
                ShowEditForm(-1);
            }
        }

    
    }
}