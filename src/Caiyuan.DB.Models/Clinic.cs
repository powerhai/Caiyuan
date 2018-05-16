using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Caiyuan.DB.Models
{
    
    public class Clinic 
    { 
        public long Id { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string Tel
        {
            get;
            set;
        }

        [MaxLength(15)]
        public string Mobile
        {
            get;
            set;
        }

        public DateTime Created { get; set; }
         
        [MaxLength(255)]
        public string Recommend
        {
            get;
            set;
        }

        [MaxLength(30)]
        public string Latitude
        {
            get;
            set;
        }

        [MaxLength(30)]
        public string Longitude
        {
            get;
            set;
        }

        //public virtual ApplicationUser Muster { get; set; }
        //public virtual Collection<ApplicationUser> Patients { get; set; }
 
       
    }
}
