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
    
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {

        public UserController()
        {

        }
        [HttpGet("getAll")]
        public ActionResult getUsersList()
        {
            return Ok();
        }
    }
}
