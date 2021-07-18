using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Database.Entity
{
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public IndexItem IndexItem { get; set; }
        public bool HasSerialNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string ActualLocation { get; set; }
        public string EAN { get; set; }
        public List<Order> Orders { get; set; }

    }
}
