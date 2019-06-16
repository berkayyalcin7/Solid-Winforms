using SolidOtomasyon.Forms.BaseForms;
using SolidOtomasyon.Show.Interfaces;
using SolidOtomasyon.Takip.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidOtomasyon.Show
{
    //BaseKartForm' BaseEditForm olarak düzenledik
    public class ShowEditForms<TForm>:IBaseFormShow where TForm : BaseEditForm 
    {

        public long ShowDialogEditForm(KartTuru kartTuru, long id) //,params object[] pre)
        {
            //Yetki Kontrolü buraya yapılacak
            //Açmış olarak formlar'ı IDisposable 'dan da implemente olabildiği için Bu işlemi yapacağız
            //using bloklarında Formun Type'ını göndreceğiz //TForm'a cast ediyoruz.
            using (var frm =(TForm)Activator.CreateInstance(typeof(TForm)))
            {
                //Enum'a değer atayarak ulaşacğız Common sınıfında bu işler yapılacak
                // ID : 0 dan büyükse Update değilse Insert olacak , 
                frm.IslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                //Id : BaseEditForm'dan gelecek Id  
                frm.Id = id;
                //Formu Yükleme işlemi
                frm.Yukle();
                //Daha Sonra Dialog olarak açılacak
                frm.ShowDialog();

                //Formun içerisinde değişiklik var ise kontrol işlemi
                return frm.RefreshYapilacak ? frm.Id : 0;


            }
        }






    }
}
