using Caiyuan.Domain;
using Caiyuan.Domain.Caiyuan.Domain;
using System;
using System.Collections.Generic;

namespace Caiyuan.ViewModels.Reservation
{
    public class ReservationInfoViewModel
    {
        public ReservationInfoViewModel ()
        {
            Reservations = new List<Reservation>();
        }
        public List<Reservation> Reservations
        {
            get;
            private set;
        }
        public class Reservation
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
