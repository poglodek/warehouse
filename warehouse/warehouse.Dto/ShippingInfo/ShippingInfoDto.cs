using System;

namespace warehouse.Dto.ShippingInfo
{
    public class ShippingInfoDto
    {
        public string ClientName { get; set; }

        public string TrackingNumber { get; set; }
        public string OrderId { get; set; }
        public string TargetLocation { get; set; }

        public bool IsInsurance { get; set; }

        public bool IsPriority { get; set; }

        public DateTime DateTime { get; set; }

        public double ShippingPrice { get; set; }
    }
}