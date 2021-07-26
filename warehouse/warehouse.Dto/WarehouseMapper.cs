using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using warehouse.Database.Entity;
using warehouse.Dto.Client;
using warehouse.Dto.Index;
using warehouse.Dto.Item;
using warehouse.Dto.Order;
using warehouse.Dto.User;

namespace warehouse.Dto
{
    public class WarehouseMapper : Profile
    {
        public WarehouseMapper()
        {
            CreateMap<Items, ItemCreateDto>().ReverseMap();
            CreateMap<Items, ItemDto>()
                .ForMember(x => x.Name, z => z.MapFrom(c =>c.IndexItem.Name))
                .ForMember(x => x.Price, z => z.MapFrom(c =>c.IndexItem.Price))
                .ForMember(x => x.Description, z => z.MapFrom(c =>c.IndexItem.Description))
                .ReverseMap();
            CreateMap<IndexItem, IndexDto>().ReverseMap();
            CreateMap<Database.Entity.Client, ClientDto>().ReverseMap();
            CreateMap<Database.Entity.User, UserDto>().ReverseMap();
            CreateMap<Database.Entity.User, UserCreatedDto>().ReverseMap();
            CreateMap<Database.Entity.Order, OrderInfoDto>().ReverseMap();
            CreateMap<Database.Entity.Order, OrderCreateDto>().ReverseMap();
            CreateMap<Database.Entity.Order, OrderListDto>().ForMember(x => x.ClientName, z => z.MapFrom(c =>c.Client.Name ))
                .ForMember(x => x.WhoCreatedEmail, z => z.MapFrom(c =>c.WhoCreated.Email ))
                .ForMember(x => x.WhoCreatedName, z => z.MapFrom(c =>c.WhoCreated.FirstName + c.WhoCreated.LastName ))
                .ReverseMap();
        }
    }
}
