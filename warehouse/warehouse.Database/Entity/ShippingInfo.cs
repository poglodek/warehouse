using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;

namespace warehouse.Database.Entity
{
    public class ShippingInfo
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string TrackingNumber { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsPriority { get; set; }
        public DateTime DateTime { get; set; }
        public double ShippingPrice { get; set; }

    }
}
