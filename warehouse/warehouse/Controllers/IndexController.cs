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
    [Route("/index")]
    [ApiController]
    public class IndexController : ControllerBase
    {

        public IndexController()
        {
            
        }
        [HttpGet("getAll")]
        public ActionResult getIndexList()
        {
            return Ok();
        }
    }
}
