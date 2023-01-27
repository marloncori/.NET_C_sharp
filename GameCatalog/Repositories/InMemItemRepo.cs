using System;
using System.Linq;
using System.Collections.Generic;
using GameCatalog.Entities;

namespace GameCatalog.Repositories
{ 
    public class InMemItemRepo
    {
        private readonly List<Item> items = new()
        // the old way was ' = new List<Item>() '
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 520, CreationDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 1340, CreationDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 968, CreationDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Silver Helmet", Price = 2571, CreationDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetAllItems()
        {
            return items;
        }

        public Item GetOneItem(Guid id)
        {
            return items.Where(items => items.Id == id).SingleOrDefault();
        }
    }
}