using EFCoreItemsProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreItemsProject.Services
{
    public class TagService
    {
        private readonly ItemsContext _context;

        public TagService(ItemsContext context)
            => _context = context;
        public ActionResult<Tag> PostTag(string itemName, string tagLabel)
        {
            var tag = _context
                .Set<Item>()
                .Include(e => e.Tags)
                .Single(e => e.Name == itemName)
                .AddTag(tagLabel);

            _context.SaveChanges();

            return tag;
        }
    }
}
