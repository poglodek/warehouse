using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Dto.Index
{
    public class IndexDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
