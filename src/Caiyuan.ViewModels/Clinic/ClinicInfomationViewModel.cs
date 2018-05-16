

using System.ComponentModel.DataAnnotations;

namespace Caiyuan.ViewModels.Clinic
{
    public class ClinicInformationViewModel
    {
        [Display(Name="菜园名称")]
        public string Title
        {
            get;
            set;
        }
        [Display(Name = "电话")]
        public string Tel
        {
            get;
            set;
        }
        [Display(Name = "手机")]
        public string Mobile
        {
            get;
            set;
        }
        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }
        [Display(Name = "介绍")]
        public string Recommend
        {
            get;
            set;
        }

        public string BusinessHours
        {
            get;
            set;
        }

    }
}
