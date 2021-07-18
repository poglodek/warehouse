using AutoMapper;
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

        private void AddItemToDataBase(Items items)
        {
            _warehouseDbContext.Items.Add(items);
            _warehouseDbContext.SaveChanges();
        }
    }
}
