using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;

namespace warehouse.Dto.Item
{
    public class ItemCreateDto
    {
        public int IndexItemId { get; set; }
        public bool HasSerialNumber { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string ActualLocation { get; set; }
        public string EAN { get; set; }
    }
}
