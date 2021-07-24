using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;

namespace warehouse.Database.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Client Client { get; set; }
        public string TargetLocation { get; set; }
        public DateTime DateTime { get; set; }
        public User WhoCreated { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }


    }
}
