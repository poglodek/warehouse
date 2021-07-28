using System;
using System.Collections.Generic;
using warehouse.Dto.Client;
using warehouse.Dto.Item;
using warehouse.Dto.User;

namespace warehouse.Dto.Order
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public ClientDto Client { get; set; }
        public string TargetLocation { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DateTime { get; set; }
        public UserDto WhoCreated { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}