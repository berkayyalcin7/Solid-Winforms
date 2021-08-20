using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using System.ComponentModel;
using System.Drawing;

namespace SolidOtomasyon.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyCardEdit:MyTextEdit
    {

        public MyCardEdit()
        {

           

            // Text' i ortalayacaktır.
            Properties.Appearance.TextOptions.HAlignment =HorzAlignment.Center;

            //Kart Numaraları Formatı 4 hane toplam 16 karakter
            Properties.Mask.MaskType = MaskType.Regular;
            // ? null olabileceği -> d : her bir rakamı temsil eder .
            Properties.Mask.EditMask = @"\d?\d?\d?\d?-\d?\d?\d?\d?-\d?\d?\d?\d?-\d?\d?\d?\d?";
            //Otomatik olarak tamamlama !!
            Properties.Mask.AutoComplete = AutoCompleteType.None;

            

            //Sabit Bir AÇıklama vericeğiz -> MyTextEdit'ten implemente edildiği için çağrılabilir
            StatusBarAciklama = "Kart No Giriniz ...";
        }





    }
}
