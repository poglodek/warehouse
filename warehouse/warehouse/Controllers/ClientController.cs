using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;

namespace warehouse.Controllers
{
    [Route("/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        public ClientController()
        {

        }
        [HttpGet("getAll")]
        public ActionResult getClientsList()
        {
            return Ok();
        }
    }
}
