using System;

namespace warehouse.Dto.ShippingInfo
{
    public class ShippingInfoCreateDto
    {
        public int ClientId { get; set; }
        public string TargetLocation { get; set; }

        public string TrackingNumber { get; set; }
        public int OrderId { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsPriority { get; set; }
        public double ShippingPrice { get; set; }
    }
}
