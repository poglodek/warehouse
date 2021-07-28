using System.ComponentModel.DataAnnotations;

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