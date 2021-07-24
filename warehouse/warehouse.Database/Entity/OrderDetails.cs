using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Database.Entity
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public virtual Items Items { get; set; }
    }
}
