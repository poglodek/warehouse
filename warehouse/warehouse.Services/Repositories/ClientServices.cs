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

        public ClientDto GetClientDtoById(int id)
        {
            var client = GetClientById(id);

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }

        public List<ClientDto> GetClientsByName(string name)
        {
            var clients = GetClientsDto();
            return  clients.Where(x => x.Name.Contains(name)).ToList();
        }
        public List<ClientDto> GetClientsByAddress(string address)
        {
            var clients = GetClientsDto();
            return clients.Where(x => x.Address.Contains(address)).ToList();
        }

        public void DeleteById(int id)
        {
            var client = GetClientById(id);

            _warehouseDbContext.Clients.Remove(client);
            _warehouseDbContext.SaveChanges();
        }

        public int CreateClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            _warehouseDbContext.Clients.Add(client);
            _warehouseDbContext.SaveChanges();

            return client.Id;
        }

        public void UpdateClient(ClientDto clientDto, int id)
        {
            var client = GetClientById(id);
            client.Address = clientDto.Address;
            client.Name = clientDto.Name;
            _warehouseDbContext.SaveChanges();
        }

        private Client GetClientById(int id)
        {
            var client = _warehouseDbContext
                .Clients
                .FirstOrDefault(x => x.Id == id);
            if (client is null) throw new NotFound("Client not found.");
            return client;
        }


        private List<ClientDto> GetClientsDto()
        {
            var clients = _warehouseDbContext.Clients.ToList();

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);
            return clientsDto;
        }
    }
}
