using System.Collections.Generic;
using warehouse.Dto.Client;

namespace warehouse.Services.IRepositories
{
    public interface IClientServices
    {
        List<ClientDto> GetAllClients();
        ClientDto GetClientDtoById(int id);
        List<ClientDto> GetClientsByName(string name);
        List<ClientDto> GetClientsByAddress(string address);
        void DeleteById(int id);
        int CreateClient(ClientDto clientDto);
        void UpdateClient(ClientDto clientDto, int id);
    }
}