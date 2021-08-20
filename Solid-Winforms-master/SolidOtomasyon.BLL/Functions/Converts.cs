using SolidOtomasyon.Takip.Model.Entities.Base.Interfaces;
using System;
using System.Linq;

namespace SolidOtomasyon.BLL.Functions
{
    public static class Converts
    {
        //Entity Convert Tanımlaayacağız...


        //Extension metot tanımlayacağız . static olarak tanımlayacağız
        // Bütün Entityler IBaseEntity'den implemente olacak
        public static TTarget EntityConvert<TTarget>(this IBaseEntity source)
        {
            //Null ise geriye otomatik olarak null gidicek
            if (source == null) return default(TTarget);
            //Instance üretmiş olduk....
            var hedef = Activator.CreateInstance<TTarget>();

            //Reflection ile entity'lerimize ulaşacağız
            var kaynakProp = source.GetType().GetProperties();
            //TTarget Property'lerine typeof ile ulaşabiliriz 
            var hedefProp = typeof(TTarget).GetProperties();

            foreach (var kp in kaynakProp)
            {
                //Değerine ulaşmış oluyoruz
                var value = kp.GetValue(source);
                //Hedef Propu -> kaynak Propun isminden bunu bul
                var hp = hedefProp.FirstOrDefault(x => x.Name == kp.Name);
                if(hp!=null)
                {
                    //ReferenceEquals -> Kaynak olarak verilecek değer string alanlar null ise "" olarak karşılaştıracağız
                    // Burada String Empty geliyorsa null yap olarak ayarladık .
                    hp.SetValue(hedef, ReferenceEquals(value, "") ? null : value);
                }
            }

            //Hedefi geri göndereceğiz.
            return hedef;
        }



    }
}
