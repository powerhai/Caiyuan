using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Caiyuan.Web.Models.Doctor
{
    public class ClinicDoctorsViewModel
    {
        
        public IEnumerable<DoctorViewModel> Doctors
        {
            get;
            set;
        }
        
    }
    public class DoctorViewModel
    {
      
        [Display(Name = "农民姓名" )]
        [Required(ErrorMessage = "{0}是必须的")]
        [MaxLength(30, ErrorMessage="{0}不可以超过30个字符")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "毕业院校")] 
        [MaxLength(30, ErrorMessage = "{0}不可以超过30个字符")]
        public string School
        {
            get;
            set;
        }

        [Display(Name = "职务")]
        [MaxLength(30, ErrorMessage = "{0}不可以超过30个字符")]
        public string Position
        {
            get;
            set;
        }

        [Display(Name = "职称")]
        [MaxLength(30, ErrorMessage = "{0}不可以超过30个字符")]
        public string Level
        {
            get;
            set;
        }


        [Display(Name="工作经验")] 
        [DataType(DataType.MultilineText)]
        [MaxLength(250,ErrorMessage="{0}不可以超过250字符")]
        public string Experience
        {
            get;
            set;
        }


        public bool IsLevelNotNull => !string.IsNullOrEmpty(Level);
        public bool IsExperienceNotNull => !string.IsNullOrEmpty(Experience);
        public bool IsSchoolNotNull => !string.IsNullOrEmpty(School);
        public bool IsPositionNotNull => !string.IsNullOrEmpty(Position);
    }
}