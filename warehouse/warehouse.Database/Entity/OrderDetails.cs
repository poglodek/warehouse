using System.ComponentModel.DataAnnotations;

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