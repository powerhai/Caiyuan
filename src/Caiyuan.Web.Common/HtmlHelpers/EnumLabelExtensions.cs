using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Caiyuan.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;

namespace Caiyuan.Web.Common.HtmlHelpers
{

    public static class EnumLabelExtensions
    {
    
        public static string LabelForEnum(this IHtmlHelper helper, Enum value )
        {
            var str = value.GetFriendlyName();
            return  str;
        }
    }
}
