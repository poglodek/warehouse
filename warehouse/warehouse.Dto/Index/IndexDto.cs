using System.ComponentModel.DataAnnotations;

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