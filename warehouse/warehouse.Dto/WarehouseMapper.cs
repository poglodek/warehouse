using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using warehouse.Database.Entity;
using warehouse.Dto.Item;

namespace warehouse.Dto
{
    public class WarehouseMapper : Profile
    {
        public WarehouseMapper()
        {
            CreateMap<Items, ItemCreateDto>().ReverseMap();
        }
    }
}
