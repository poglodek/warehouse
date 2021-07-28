using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;
using warehouse.Exceptions;
using warehouse.Exceptions.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class ItemServices : IItemServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;

        public ItemServices(WarehouseDbContext warehouseDbContext,
            IUserContextServices userContextServices,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _userContextServices = userContextServices;
            _mapper = mapper;
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

        public List<ItemDto> GetItemList(string searchingParse, int pageNumber, int quantity)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (quantity == 0) quantity = 5;

            List<Items> items;
            items = string.IsNullOrEmpty(searchingParse) ? SearchWithOutParse(pageNumber, quantity) : SearchWithParse(searchingParse, pageNumber, quantity);

            var itemsDto = GetMappedItemsDto(
                items
                .ToList());
            return itemsDto;
        }

        public void DeleteById(int id)
        {
            if (!IfUserCreated(_userContextServices.GetUserId(), id)) throw new ForbiddenException("Forbidden");
            var item = GetItemById(id);
            _warehouseDbContext.Items.Remove(item);
            _warehouseDbContext.SaveChanges();
        }

        public List<ItemDto> GetByEan(string ean)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Where(x => x.EAN.Contains(ean))
                .ToList();
            return GetMappedItemsDto(items);
        }

        public List<ItemDto> GetByLocation(string location)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Where(x => x.EAN.Contains(location))
                .ToList();
            return GetMappedItemsDto(items);
        }

        public List<ItemDto> GetBySerialNumber(string serialNumber)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Where(x => x.SerialNumber == serialNumber)
                .ToList();
            return GetMappedItemsDto(items);
        }

        public void Update(ItemDto itemDto, int id)
        {
            if (!IfUserCreated(_userContextServices.GetUserId(), id)) throw new ForbiddenException("Forbidden");
            var item = GetItemById(id);
            var updateItem = _mapper.Map<Items>(itemDto);
            var updatedItem = ItemUpdate(item, updateItem);
        }

        private Items ItemUpdate(Items itemInDb, Items updateItem)
        {
            itemInDb.ActualLocation = updateItem.ActualLocation;
            itemInDb.EAN = updateItem.EAN;
            itemInDb.HasSerialNumber = updateItem.HasSerialNumber;
            itemInDb.Quantity = updateItem.Quantity;
            itemInDb.SerialNumber = updateItem.SerialNumber;
            _warehouseDbContext.SaveChanges();
            return itemInDb;
        }

        private ItemDto GetMappedItemDto(Items item)
        {
            if (item is null) throw new NotFound("Item not found.");
            var itemDto = _mapper.Map<ItemDto>(item);

            return itemDto;
        }

        private List<ItemDto> GetMappedItemsDto(List<Items> items)
        {
            if (items is null) throw new NotFound("Items not found.");
            var itemsDto = _mapper.Map<List<ItemDto>>(items);

            return itemsDto;
        }

        private void AddItemToDataBase(Items items)
        {
            _warehouseDbContext.Items.Add(items);
            _warehouseDbContext.SaveChanges();
        }

        public ItemDto GetItemDtoById(int id)
        {
            var item = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .FirstOrDefault(x => x.Id == id);

            return GetMappedItemDto(item);
        }

        public Items GetItemById(int id)
        {
            var item = _warehouseDbContext
                .Items
                .FirstOrDefault(x => x.Id == id);
            if (item is null) throw new NotFound("Item not found.");
            return item;
        }

        private IndexItem GetIndexItemById(int id)
        {
            return _warehouseDbContext
                .IndexItems
                .FirstOrDefault(x => x.Id == id);
        }

        private List<Items> SearchWithParse(string searchingParse, int pageNumber, int quantity)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Where(x => x.IndexItem.Name.Contains(searchingParse))
                .Skip(pageNumber - 1)
                .Take(quantity)
                .ToList(); ;
            return items;
        }

        private List<Items> SearchWithOutParse(int pageNumber, int quantity)
        {
            var items = _warehouseDbContext
                .Items
                .Include(x => x.IndexItem)
                .Skip(pageNumber - 1)
                .Take(quantity)
                .ToList(); ;
            return items;
        }
        private bool IfUserCreated(int userId, int itemId)
        {
            var whoCreatedId = GetItemById(itemId)
                .WhoCreated
                .Id;
            var role = _userContextServices.GetUser.FindFirst(x => x.Type == ClaimTypes.Role).Value;
            if (userId == whoCreatedId) return true;
            else if (role == "Admin") return true;
            return false;
        }
    }
}