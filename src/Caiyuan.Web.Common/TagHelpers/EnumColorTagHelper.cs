using System;
using Caiyuan.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Caiyuan.Web.Common.TagHelpers
{
    [HtmlTargetElement("div")]
    [HtmlTargetElement("span")]
    [HtmlTargetElement("p")]
    public class EnumColorTagHelper : TagHelper
    {

        [HtmlAttributeName("enum-color")]
        public Enum EnumColor
        {
            get;
            set;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        { 
            if(EnumColor == null)
                return;
            var color = EnumColor.GetColor();
            output.Attributes.SetAttribute("style", "color:" + color);
        }
    }
}