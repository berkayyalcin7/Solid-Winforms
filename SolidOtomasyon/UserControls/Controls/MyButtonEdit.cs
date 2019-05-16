using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SolidOtomasyon.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
namespace SolidOtomasyon.UserControls.Controls
{

    [ToolboxItem(true)]
    public class MyButtonEdit : ButtonEdit,IStatusBarKisaYol
    {

        // 1 -> Componentimize İşlevsellik kazandıracağız ....
        public MyButtonEdit()
        {
            // 2 ->  Propertyler


            // 3 -> Text alanına herhangi bir yazı yazılamaz. 
            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            Properties.AppearanceFocused.BackColor = Color.LightCyan;
           
        }

        // 4 -> Enter ile bir sonraki nesneye Focuslanma metodu default False gelir -> True Çeviriyoruz.
        public override bool EnterMoveNextControl { get; set; } = true;

        // 5-> Implemente edilmiş property'ler IStatusBarKisaYol'dan
        public string StatusBarKisaYol { get; set; } = "F4 :";
        public string StatusBarKisaYolAciklama { get; set ; }

        public string StatusBarAciklama { get; set; }

        #region Events

        //10 -> Yerel private bir değişken olduğu için _id olarak tanımladı -> Değerleri Bu property'de saklayacağız;
        private long? _id;

        // Property' Listesinde Bu Id Değeri Gözükmemesi için Browsable Attr'si atıyoruz. False
        [Browsable(false)]
        public long? Id
        {
            // 11 ) ' => ' sembol  'return' yerine kullanılabilir
            get => _id;
            set
            {
                //Eski değeri Alıyoruz
                var oldValue = _id;
                //Yeni gelecek değer 
                var newValue = value;

                // Eşit ise işlem yapma return ..
                if (newValue == oldValue) return;

                _id = value;

                // 12 ) 7.1 maddedeki'nin alternatifi ID değişme Event'ini tetiklemiş olduk. -> IdChanged?.Invoke ile Null değilse eventi tetikle  kontrolü sağlıyoruz
                IdChanged(this, new IdChangedEventArgs(oldValue, newValue));

            }
        }

        // 7 ->  ID ' özelliği ile değişimleri yakalayabileceğiz -> ID Changed Event Delegate Tanımlayacağız -> Daha Sonra Tanımladığımız  IdChangedEventArgs sınıfını Handler ediyoruz.

        public event EventHandler<IdChangedEventArgs> IdChanged = delegate { }; // 7.1 -> asla NULL'a düşmemek istersek =delegate{  }; ile boş bir delege atayacak. 

        #endregion

    }

    // 8 ->  Geriye 2 Tane parametre gönderecek - Old Value - New Value Şeklinde
    public class IdChangedEventArgs : EventArgs
    {
        // 9-> Public özellikler büyük harflerle tanımlanması uygun olur ...
        public long? OldValue { get;}
        public long? NewValue { get;}


        public IdChangedEventArgs(long? oldValue , long? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }




}
