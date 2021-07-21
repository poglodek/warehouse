using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Index;
using warehouse.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class IndexServices : IIndexServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;

        public IndexServices(WarehouseDbContext warehouseDbContext,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }

        public List<IndexDto> GetIndexes()
        {
            var indexes = _warehouseDbContext
                .IndexItems
                .ToList();

            var indexesDto = _mapper.Map<List<IndexDto>>(indexes);
            return indexesDto;
        }

        public IndexDto GetIndexById(int id)
        {
            var index = _warehouseDbContext
                .IndexItems
                .FirstOrDefault(x => x.Id == id);

            if (index is null) throw new NotFound("ItemIndex not found.");

            var indexDto = _mapper.Map<IndexDto>(index);
            return indexDto;

        }

        public List<IndexDto> GetIndexByName(string name)
        {
            var indexes = _warehouseDbContext
                .IndexItems
                .Where(x => x.Name.Contains(name));

            if (indexes is null) throw new NotFound("ItemIndex not found.");

            var indexesDto = _mapper.Map<List<IndexDto>>(indexes);
            return indexesDto;
        }

        public int Create(IndexDto indexDto)
        {
            var index = _mapper.Map<IndexItem>(indexDto);
            _warehouseDbContext.IndexItems.Add(index);
            _warehouseDbContext.SaveChanges();
            return index.Id;

        }
    }
}
