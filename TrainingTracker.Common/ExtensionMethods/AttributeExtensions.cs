using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.Common.ExtensionMethods
{
    public static class AttributeExtensions
    {
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }

        public static string GetDisplayName(this object item, string defaultName = null)
        {
            if (item == null)
            {
                return string.Empty;
            }

            if (String.IsNullOrWhiteSpace(defaultName))
            {
                defaultName = item.GetType().Name;
            }

            var displayAttr = item.GetType().GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            if (displayAttr == null)
            {
                return defaultName;
            }

            return displayAttr.Name;
        }
    }
}
