using System;

namespace warehouse.Dto.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string TargetLocation { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DateTime { get; set; }
        public string WhoCreatedName { get; set; }
        public string WhoCreatedEmail { get; set; }
    }
}