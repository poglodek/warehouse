using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;
using warehouse.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class ItemServices : IItemServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;


        public ItemServices(WarehouseDbContext warehouseDbContext,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }

        private IndexItem GetIndexItemById(int id)
        {
            return _warehouseDbContext
                .IndexItems
                .FirstOrDefault(x => x.Id == id);
        }

        public Items CreateNewItem(ItemCreateDto itemCreateDto)
        {
            var newItem = _mapper.Map<Items>(itemCreateDto);
            var itemIndex = GetIndexItemById(itemCreateDto.IndexItemId);
            if (itemIndex is null) throw new NotFound("ItemIndex not found.");
            newItem.IndexItem = itemIndex;
            AddItemToDataBase(newItem);
            return newItem;
        }

        public List<ItemDto> GetItemList(string searchingParse = "", int pageNumber = 1, int quantity = 5)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Where(x => x.IndexItem.Name.Contains(searchingParse))
                .Skip(pageNumber - 1)
                .Take(quantity)
                .ToList();
            var itemsDto = _mapper.Map<List<ItemDto>>(items);
            return itemsDto;
        }

        private void AddItemToDataBase(Items items)
        {
            _warehouseDbContext.Items.Add(items);
            _warehouseDbContext.SaveChanges();
        }
    }
}
