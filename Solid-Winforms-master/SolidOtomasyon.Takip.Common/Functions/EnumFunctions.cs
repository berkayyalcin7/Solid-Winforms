using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Common.Functions
{
    public static class EnumFunctions
    {
        //Diğer functionlarda kullanacağımız private -> T nin attr olduğunu belirtiyoruz.
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) return null;
            //gelen enum value'sini ToStringe çeviriyoruz. MemberLara Ulaşabilmek için
            //Get Memberla içinde dolaşıyoruz.
            var memberInfo = value.GetType().GetMember(value.ToString());

            //memberInfo dizi olarak gelcek olsa bile , 1 tane attr gelicek 0. indexi alıyoruz. 2. parametre kalıtım türünden attr alınacakmı false 
            var attribute = memberInfo[0].GetCustomAttributes(typeof(T), false);

            //T ye cast edip return ediyoruz.
            return (T)attribute[0];
        }

        //Enumlara Description'dan ulaşma
        public static string ToName(this Enum value)
        {
            if (value == null) return null;

            //Description Attr'si : DescriptionAttribute'dan türemiş
            var attribute = value.GetAttribute<DescriptionAttribute>();

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static ICollection GetEnumDescriptionList<T>()
        {
            //Buraya Gelen T Türünde bir tip gelicek T içindeki üyeleri dolaşıp Description attr'si olanları seçecektir. Kalıtım yoluyla gelenleride alacak
            //Select ile bunları çekecek.
            return typeof(T).GetMembers()
                .SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
                .Cast<DescriptionAttribute>())
                .Select(x => x.Description).ToList();
        }

        public static T GetEnum<T>(this string description)
        {
            //Description vereren Enum'ı alacağız -> Örnek EvetHayir Enumı
            var enumNames = Enum.GetNames(typeof(T));

            //Descriptionları karşılaştırıyoruz
            foreach (var e in enumNames.Select(x => Enum.Parse(typeof(T), x)).Where(y => description == ToName((Enum)y)))
            {
                return (T)e;
            }
            
            return default(T);
        }
    }
}
