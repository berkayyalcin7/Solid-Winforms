using System;

namespace SolidOtomasyon.Takip.Model.Attributes
{
    public class ZorunluAlan:Attribute
    {
        public string Description { get; }

        public string ControlName { get; }

        /// <summary>
        /// Validation İşlemleri sırasında Zorunlu olan Alanları işaretlemek için kullanılacak
        /// </summary>
        /// <param name="description"> Uyarı Mesajında Gösterilecek olan Açıklama  </param>
        /// <param name="controlName"> Uyarı sonrası Hangi Alana Focuslanacağı </param>

        public ZorunluAlan(string description , string controlName)
        {
        
            Description = description;
            ControlName = controlName;

        }
    }
}
