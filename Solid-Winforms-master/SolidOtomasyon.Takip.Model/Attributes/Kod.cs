using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Model.Attributes
{
    public class Kod:Attribute
    {
        public string Description { get; }

        public string ControlName { get; }

        /// <summary>
        /// Validation İşlemleri sırasında Zorunlu olan Alanları işaretlemek için kullanılacak
        /// </summary>
        /// <param name="description"> Uyarı Mesajında Gösterilecek olan Açıklama  </param>
        /// <param name="controlName"> Uyarı sonrası Hangi Alana Focuslanacağı </param>

        public Kod(string description , string controlName)
        {
            Description = description;
            ControlName = controlName;
        }





    }
}
