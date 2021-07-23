using System.Collections.Generic;
using warehouse.Dto.User;

namespace warehouse.Services.IRepositories
{
    public interface IUserServices
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserDtoById(int id);
    }
}