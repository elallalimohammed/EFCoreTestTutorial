using EFCoreItemsProject.Models;
using EFCoreItemsProject.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class ItemsUnitTest:ItemsControllerTest
    {

        public ItemsUnitTest()
        : base(
            new DbContextOptionsBuilder<ItemsContext>()
                .UseSqlite("Filename=Test.db")
                .Options)
        {
        }
        [Fact]
        public void Can_get_items()
        {
            using (var context = new ItemsContext(ContextOptions))
            {
                var controller = new ItemService(context);

                var items = controller.Get().ToList();

                Assert.Equal(3, items.Count);
                Assert.Equal("ItemOne", items[0].Name);
                Assert.Equal("ItemThree", items[1].Name);
                Assert.Equal("ItemTwo", items[2].Name);
            }
        }

        [Fact]
        public void Can_add_item()
        {
            using (var context = new ItemsContext(ContextOptions))
            {
                var controller = new ItemService(context);

                var item = controller.AddItem("ItemFour").Value;

                Assert.Equal("ItemFour", item.Name);
            }

            using (var context = new ItemsContext(ContextOptions))
            {
                var item = context.Set<Item>().Single(e => e.Name == "ItemFour");

                Assert.Equal("ItemFour", item.Name);
                Assert.Equal(0, item.Tags.Count);
            }
        }

        [Fact]
        public void Can_add_tag()
        {
            using (var context = new ItemsContext(ContextOptions))
            {
                var controller = new TagService(context);

                var tag = controller.PostTag("ItemTwo", "Tag21").Value;

                Assert.Equal("Tag21", tag.Label);
                Assert.Equal(1, tag.Count);
            }

            using (var context = new ItemsContext(ContextOptions))
            {
                var item = context.Set<Item>().Include(e => e.Tags).Single(e => e.Name == "ItemTwo");

                Assert.Equal(1, item.Tags.Count);
                Assert.Equal("Tag21", item.Tags[0].Label);
                Assert.Equal(1, item.Tags[0].Count);
            }
        }

        [Fact]
        public void Can_add_tag_when_already_existing_tag()
        {
            using (var context = new ItemsContext(ContextOptions))
            {
                var controller = new TagService(context);

                var tag = controller.PostTag("ItemThree", "Tag32").Value;

                Assert.Equal("Tag32", tag.Label);
                Assert.Equal(3, tag.Count);
            }

            using (var context = new ItemsContext(ContextOptions))
            {
                var item = context.Set<Item>().Include(e => e.Tags).Single(e => e.Name == "ItemThree");

                Assert.Equal(2, item.Tags.Count);
                Assert.Equal("Tag31", item.Tags[0].Label);
                Assert.Equal(3, item.Tags[0].Count);
                Assert.Equal("Tag32", item.Tags[1].Label);
                Assert.Equal(3, item.Tags[1].Count);
            }
        }


    }
}
