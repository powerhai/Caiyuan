using System.ComponentModel.DataAnnotations;

namespace Caiyuan.Web.Models.Clinic
{
    public class CreateClinicByMasterViewModel
    {
      
        [Display(Name = "菜园名称" )]
        [Required(ErrorMessage = "{0}是必须的")]
        [MaxLength(30, ErrorMessage="菜园名称不可以超过30个字符")]
        public string Title
        {
            get;
            set;
        }

        [Display(Name = "地址" )]
        [Required(ErrorMessage = "{0}是必须的")]
        [MaxLength(50, ErrorMessage = "菜园地址不可以超过50个字符")]
        public string Address
        {
            get;
            set;
        }
        [Display(Name = "电话")]
        [Required(ErrorMessage = "{0}是必须的")]
        [RegularExpression(@"\d{1,4}-\d{6,9}",ErrorMessage="电话号码格式错误，请参考如下格式：XXXX-XXXXXXXX")]
        public string Tel
        {
            get;
            set;
        }
        [Display(Name = "手机")]
        [Required(ErrorMessage = "{0}是必须的")]
        [RegularExpression(@"\d{11}", ErrorMessage = "{0}格式错误，请参考如下格式：XXXX-XXXXXXXX")]
        public string Mobile
        {
            get;
            set;
        }

        [Display(Name="菜园介绍")] 
        [DataType(DataType.MultilineText)]
        [MaxLength(250,ErrorMessage="{0}不可以超过250字符")]
        public string Recommend
        {
            get;
            set;
        }

    }
}