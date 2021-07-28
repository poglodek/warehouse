using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace warehouse.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string Phone { get; set; }
        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
        public List<Items> Items { get; set; }
        public List<ShippingInfo> ShippingInfo { get; set; }
    }
}