using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Reflection;
using System.Threading.Tasks;

namespace Caiyuan.Common
{
     
    public static class EnumExtensions
    {
        public static string GetFriendlyName (this Enum value)
        { 
            var dis = value.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayAttribute>();
            if (dis == null)
                return value.ToString();
            return dis.Name;
        }
        public static string GetDescription(this Enum value)
        {
            var dis = value.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayAttribute>();
            if (dis == null)
                return value.ToString();
            return dis.Description; 
        }
        public static string GetColor (this Enum value)
        {
            var dis = value?.GetType().GetField(value.ToString()).GetCustomAttribute<EnumColorAttribute>();
            return dis == null ? "black" : dis.Color;
        }
    }
}
