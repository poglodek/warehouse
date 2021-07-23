using System.Collections.Generic;
using warehouse.Dto.Client;

namespace warehouse.Services.IRepositories
{
    public interface IClientServices
    {
        List<ClientDto> GetAllClients();
        ClientDto GetClientById(int id);
    }
}