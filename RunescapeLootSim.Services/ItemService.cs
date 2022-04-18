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
        private readonly string _userId;
        public ItemService(string userId)
        {
            _userId = userId;
        }

        //public string UserId { get; }

        public bool CreateItem(ItemCreate model)
        {
            var entity =
                new Item()
                {
                    UserId = _userId,
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

        public ItemDetail GetItemsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .SingleOrDefault(e => e.ItemId == id && e.UserId == _userId);
                        return
                                new ItemDetail
                                {
                                    ItemId = entity.ItemId,
                                    Name = entity.Name,
                                    Damage = entity.Damage,
                                    DropRate = entity.DropRate
                                };
                
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

        public bool UpdateItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(e => e.ItemId == model.ItemId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.Damage = model.Damage;
                entity.DropRate = model.DropRate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Items
                        .Single(item => item.ItemId == itemId && _userId == item.UserId);

                ctx.Items.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
