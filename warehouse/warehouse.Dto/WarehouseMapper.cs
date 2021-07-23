﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using warehouse.Database.Entity;
using warehouse.Dto.Client;
using warehouse.Dto.Index;
using warehouse.Dto.Item;
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
        }
    }
}
