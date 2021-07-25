using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Database.Entity;
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
        public DateTime DateTime { get; set; }
        public UserDto WhoCreated { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
