using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;

namespace warehouse.Database.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public Items Items { get; set; }
        public Client Client { get; set; }
        public string TargetLocation { get; set; }
        public DateTime DateTime { get; set; }
        public User WhoCompleted { get; set; }

    }
}
