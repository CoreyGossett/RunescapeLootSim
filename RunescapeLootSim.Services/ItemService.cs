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


    }
}
