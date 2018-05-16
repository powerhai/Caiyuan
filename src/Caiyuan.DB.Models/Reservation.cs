using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Domain;
using Caiyuan.Domain.Caiyuan.Domain;

namespace Caiyuan.DB.Models
{
    public class Reservation
    {
        public  long Id
        {
            get;
            set;
        }
        public DateTime Created
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public string Objects
        {
            get;
            set;
        }

        public ReservationStatus Status
        {
            get;
            set;
        }
    }
}
