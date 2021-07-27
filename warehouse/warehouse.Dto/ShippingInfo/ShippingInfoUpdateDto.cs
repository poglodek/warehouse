﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Dto.ShippingInfo
{
    public class ShippingInfoUpdateDto
    {
        public string TargetLocation { get; set; }
        public string TrackingNumber { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsPriority { get; set; }
        public double ShippingPrice { get; set; }
    }
}
