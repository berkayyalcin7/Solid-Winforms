using System;
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
        private static T GetAttribute<T>(this Enum value) where T:Attribute
        {
            if (value == null) return null;
            //gelen enum value'sini ToStringe çeviriyoruz. MemberLara Ulaşabilmek için
            //Get Memberla içinde dolaşıyoruz.
            var memberInfo = value.GetType().GetMember(value.ToString());

            //memberInfo dizi olarak gelcek olsa bile , 1 tane attr gelicek 0. indexi alıyoruz. 2. parametre kalıtım türünden attr alınacakmı false 
            var attribute = memberInfo[0].GetCustomAttributes(typeof(T),false);

            //T ye cast edip return ediyoruz.
            return (T)attribute[0];
        }

        //Enumlara Description'dan ulaşma
        public static string ToName(this Enum value)
        {
            if (value == null) return null;

            //Description Attr'si : DescriptionAttribute'dan türemiş
            var attribute = value.GetAttribute<DescriptionAttribute>();

            return attribute==null ? value.ToString() : attribute.Description;
        }
    }
}
