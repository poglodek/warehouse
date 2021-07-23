using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Client;
using warehouse.Dto.Index;
using warehouse.Exceptions;
using warehouse.Exceptions.Exceptions;
using warehouse.Services.IRepositories;

namespace warehouse.Services.Repositories
{
    public class ClientServices : IClientServices
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        private readonly IMapper _mapper;

        public ClientServices(WarehouseDbContext warehouseDbContext,
            IMapper mapper)
        {
            _warehouseDbContext = warehouseDbContext;
            _mapper = mapper;
        }


        public List<ClientDto> GetAllClients()
        {
            var clients =  _warehouseDbContext.Clients.ToList();

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return clientsDto;
        }

        public ClientDto GetClientById(int id)
        {
            var client = _warehouseDbContext
                .IndexItems
                .FirstOrDefault(x => x.Id == id);

            if (client is null) throw new NotFound("ItemIndex not found.");

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }
       
    }
}
