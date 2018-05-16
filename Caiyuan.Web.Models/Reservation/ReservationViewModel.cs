using System;
using System.ComponentModel.DataAnnotations;

namespace Caiyuan.Web.Models.Reservation
{
    public class ReservationViewModel
    {
        public long ID
        {
            get;
            set;
        }

        [Display(Name="项目")]
        public string Objects
        {
            get;
            set;
        }
        [Display(Name="预约时间")]
        public DateTime DateTime
        {
            get;
            set;
        }
    }
}