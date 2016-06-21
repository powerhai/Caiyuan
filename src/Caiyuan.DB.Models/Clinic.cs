using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Caiyuan.DB.Models
{
    
    public class Clinic 
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual ApplicationUser Muster { get; set; }
        public virtual Collection<ApplicationUser> Patients { get; set; } 

        
    }
}
