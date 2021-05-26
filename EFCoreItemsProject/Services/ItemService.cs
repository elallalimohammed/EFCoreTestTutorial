using EFCoreItemsProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreItemsProject.Services
{
    public class ItemService
    {
        private readonly ItemsContext _context;

        public ItemService(ItemsContext context)
            => _context = context;
        public IEnumerable<Item> Get()
    => _context.Set<Item>().Include(e => e.Tags).OrderBy(e => e.Name);


        public ActionResult<Item> AddItem(string itemName)
        {
            var item = _context.Add(new Item(itemName)).Entity;

            _context.SaveChanges();

            return item;
        }

        public ActionResult<Item> DeleteItem(string itemName)
        {
            var item = _context
                .Set<Item>()
                .SingleOrDefault(e => e.Name == itemName);

              _context.Remove(item);
            _context.SaveChanges();

            return item;
        }
    }
}
