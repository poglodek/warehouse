using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using warehouse.Database;
using warehouse.Dto.Index;
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
    }
}
