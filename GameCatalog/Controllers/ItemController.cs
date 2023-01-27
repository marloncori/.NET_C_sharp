using System;
using Microsoft.AspNetCore.Mvc;
// this import ApiController and ControllerBase features
using GameCatalog.Repositories;
using GameCatalog.Entities;
using System.Collections.Generic;

namespace GameCatalog.Controllers
{
    // GET /items
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase 
    {
        private readonly InMemItemRepo itemRepo;

        public ItemController()
        {
            itemRepo = new InMemItemRepo();
        }

        [HttpGet]
        public IEnumerable<Item> FindAllItems()
        {
            var items = itemRepo.GetAllItems();
            return items;
        }

        //GET  /items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> FindOneItem(Guid id)
        {
            if(id == null)
            {
                throw new InvalidParamError(" The ID parameter cannot be null!"); 
            }
            var item = itemRepo.GetOneItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return item;
        }

    }
}