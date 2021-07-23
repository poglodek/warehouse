using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;

        public ClientController(IClientServices clientServices)
        {
            _clientServices = clientServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<Client>> GetClientsList()
        {
            var clients = _clientServices.GetAllClients();
            return Ok();
        }
        [HttpGet("Get/{id}")]
        public ActionResult<Client> GetById([FromRoute]int id)
        {
            var client = _clientServices.GetClientById(id);
            return Ok(client);
        }
    }
}
