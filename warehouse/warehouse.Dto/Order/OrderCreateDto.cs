namespace warehouse.Dto.Order
{
    public class OrderCreateDto
    {
        public string TargetLocation { get; set; }
        public int ClientId { get; set; }
        public string OrderStatus { get; set; }
    }
}