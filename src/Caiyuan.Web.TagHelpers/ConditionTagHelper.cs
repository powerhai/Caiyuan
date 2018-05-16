using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Caiyuan.Web.TagHelpers
{


    [HtmlTargetElement("div")]
    [HtmlTargetElement("style")]
    [HtmlTargetElement("p")]
    public class ConditionTagHelper : TagHelper
    {
        public bool? Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // 如果设置了condition，并且该值为false，则不渲染该元素
            if (Condition.HasValue && !Condition.Value)
            {
                output.SuppressOutput();
            }
        }
    }
}