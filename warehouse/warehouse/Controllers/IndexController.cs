﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using warehouse.Database;
using warehouse.Database.Entity;
using warehouse.Dto.Index;
using warehouse.Services.IRepositories;

namespace warehouse.Controllers
{
    [Route("/Index")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IIndexServices _indexServices;

        public IndexController(IIndexServices indexServices)
        {
            _indexServices = indexServices;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<IndexDto>> getIndexList()
        {
            var indexes = _indexServices.GetIndexes();
            return Ok(indexes);
        }
        [HttpGet("Get/{id}")]
        public ActionResult<IndexDto> getIndexById([FromRoute]int id)
        {
            var index = _indexServices.GetIndexById(id);
            return Ok(index);
        }
        [HttpGet("Get")]
        public ActionResult<List<IndexDto>> getIndexByName([FromQuery]string Name)
        {
            var indexes = _indexServices.GetIndexByName(Name);
            return Ok(indexes);
        }
        [HttpPost("Create")]
        public ActionResult Create([FromBody] IndexDto index)
        {
            var indexId = _indexServices.Create(index);
            return Created("Index/Get/" + indexId, null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            
            _indexServices.Delete(id);
            return NoContent();
        }
    }
}
