using RunescapeLootSim.Data;
using RunescapeLootSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Services
{
    public class ItemService
    {
        public ItemService(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }

        public bool CreateItem(ItemCreate model)
        {
            var entity =
                new Item()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Damage = model.Damage,
                    DropRate = model.DropRate
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItemListItem> GetItemsByUser(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Items
                        .Where(item => userId == item.UserId)
                        .Select(
                            item =>
                                new ItemListItem
                                {
                                    ItemId = item.ItemId,
                                    Name = item.Name,
                                    Damage = item.Damage,
                                    DropRate = item.DropRate
                                });
                return query.ToArray();
            }
        }

        public IEnumerable<ItemListItem> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Items
                        .Select(
                            item =>
                                new ItemListItem
                                {
                                    ItemId = item.ItemId,
                                    Name = item.Name,
                                    Damage = item.Damage,
                                    DropRate = item.DropRate
                                });
                return query.ToArray();
            }
        }

        public bool UpdateItem(ItemEdit model, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(e => e.ItemId == model.ItemId && userId == model.UserId);

                entity.Name = model.Name;
                entity.Damage = model.Damage;
                entity.DropRate = model.DropRate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int itemId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(item => item.ItemId == itemId && userId == item.UserId);

                ctx.Items.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
