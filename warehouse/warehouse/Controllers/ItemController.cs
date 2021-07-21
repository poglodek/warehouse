using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Item;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [ApiController]
    [Route("/Items")]
    
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<ItemDto>> GetItemsList([FromQuery]string searchingParse, [FromQuery]int pageNumber, [FromQuery]int quantity)
        {
            var items = _itemServices.GetItemList(searchingParse, pageNumber, quantity);
            return Ok(items);
        }

        [HttpPost("Create")]
        public ActionResult<ItemDto> CreateItem([FromBody]ItemCreateDto itemCreateDto)
        {

           var item = _itemServices.CreateNewItem(itemCreateDto);

           return Created("/Get/" + item.Id, null);
        }
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById([FromRoute]int id)
        {
            var item = _itemServices.GetItemDtoById(id);
            
            return Ok(item);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
             _itemServices.DeleteById(id);

            return NoContent();
        }
        [HttpGet("EAN/{ean}")]
        public ActionResult<List<ItemDto>> GetByEan([FromRoute] string ean)
        {
            var items = _itemServices.GetByEan(ean);

            return Ok(items);
        }
        [HttpGet("SerialNumber")]
        public ActionResult<List<ItemDto>> GetBySerialNumber([FromQuery] string serialNumber)
        {
            var items = _itemServices.GetBySerialNumber(serialNumber);

            return Ok(items);
        }
        [HttpGet("location")]
        public ActionResult<List<ItemDto>> GetByLocation([FromQuery] string location)
        {
            var items = _itemServices.GetByLocation(location);

            return Ok(items);
        }
        [HttpPatch("{id}")]
        public ActionResult Update([FromBody]ItemDto itemDto, [FromRoute]int id)
        {
            _itemServices.Update(itemDto, id);

            return Ok();
        }

    }
}
