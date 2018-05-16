using Caiyuan.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Caiyuan.Domain
{
    namespace Caiyuan.Domain
    {
        public enum ReservationStatus
        {
            [Display(Name = "初始")]
            [EnumColor("Red")]
            Init,

            [EnumColor("Blue")]
            Deal
        }
    }
}
