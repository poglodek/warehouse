using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Client;
using warehouse.Dto.User;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<UserDto>> GetClientsList()
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById([FromRoute] int id)
        {
            var user = _userServices.GetUserDtoById(id);
            return Ok(user);
        }


    }
}
