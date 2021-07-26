using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Dto.Order
{
    public class OrderCreateDto
    {
        public string TargetLocation { get; set; }
        public int ClientId { get; set; }
        public string OrderStatus { get; set; }
    }
}
