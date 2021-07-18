using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet("getAll")]
        public ActionResult getItemsList()
        {
            return Ok();
        }

        [HttpPost("create")]
        public ActionResult CreateItem([FromBody]ItemCreateDto itemCreateDto)
        {
            return Ok();
        }
    }
}
