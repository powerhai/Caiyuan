using System;
using System.Collections.Generic;
using Caiyuan.Domain;
using Caiyuan.Domain.Caiyuan.Domain;

namespace Caiyuan.Web.Models.Reservation
{
    public class ReservationInfoViewModel
    {
        public ReservationInfoViewModel ()
        {
            Reservations = new List<ReservationData>();
        }
        public List<ReservationData> Reservations
        {
            get;
            private set;
        }
        public class ReservationData
        {
            public string Objects
            {
                get;
                set;
            }

            public DateTime DateTime
            {
                get;
                set;
            }

            public ReservationStatus Status
            {
                get;
                set;
            }
            public long ID { get; set; }
        }
    }
}
