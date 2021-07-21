using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace warehouse.Database.Entity
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
        public List<ShippingInfo> ShippingInfo { get; set; }

    }
}
