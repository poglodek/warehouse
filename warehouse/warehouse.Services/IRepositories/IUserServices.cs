using System.Collections.Generic;
using warehouse.Database.Entity;
using warehouse.Dto.User;

namespace warehouse.Services.IRepositories
{
    public interface IUserServices
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserDtoById(int id);
        List<UserDto> GetUserDtoByName(string name);
        List<UserDto> GetUserDtoByEmail(string email);
        List<UserDto> GetUserDtoByPhone(string phone);
        void DeleteById(int id);
        int RegisterUser(UserCreatedDto user);
        string LoginUser(UserLoginDto user);
        public User GetUserById(int id);
    }
}