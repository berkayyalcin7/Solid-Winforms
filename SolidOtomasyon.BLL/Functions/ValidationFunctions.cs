using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.BLL.Functions
{
    public static class ValidationFunctions
    {

        public static List<PropertyAttribute<TAttribute>> GetPropertyAttributeFromType<TAttribute>(this Type entityType) where TAttribute:Attribute
        {
            var list = new List<PropertyAttribute<TAttribute>>();
            //Gelen entity Propertylerini aldık
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                //true diyerek kalıtım yoluyla gelen attr li dahil ettik
                var attributes = property.GetCustomAttributes<TAttribute>(true).ToList();
                if (!attributes.Any())
                {
                    continue;
                }
                list.AddRange(attributes.Select(x => new PropertyAttribute<TAttribute>(property, x)));
            }
            //Interfacelere ulaşıyoruz
            var interfaces = entityType.GetInterfaces();

            foreach (var intf in interfaces)
            {
                list.AddRange(intf.GetPropertyAttributeFromType<TAttribute>());
            }

            return list;
        }



        public class PropertyAttribute<TAttribute>
        {
            public PropertyInfo Property { get; }

            public TAttribute Attribute { get; }


            public PropertyAttribute(PropertyInfo property, TAttribute attribute)
            {
                Property = property;
                Attribute = attribute;
            }

        }







    }
}
