using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace warehouse.Database.Entity
{
    public class Items
    {
        [Key]
        public int Id { get; set; }

        public IndexItem IndexItem { get; set; }

        public bool HasSerialNumber { get; set; }

        public string SerialNumber { get; set; }
        public User WhoCreated { get; set; }
        public int Quantity { get; set; }

        public string ActualLocation { get; set; }
        public string EAN { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}