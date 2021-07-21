using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;

namespace warehouse.Dto.Item
{
    public class ItemCreateDto
    {
        [Required]
        public int IndexItemId { get; set; }
        [Required]
        public bool HasSerialNumber { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ActualLocation { get; set; }

        public string EAN { get; set; }
    }
}
