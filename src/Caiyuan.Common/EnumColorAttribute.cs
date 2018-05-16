using System; 
namespace Caiyuan.Common
{
    
    public class EnumColorAttribute : Attribute
    {
        public string Color
        {
            get;
            private set;
        }
        public EnumColorAttribute (string color )
        {
            Color = color;
        }
    }
}
