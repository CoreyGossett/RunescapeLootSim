using RunescapeLootSim.Data;
using RunescapeLootSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Services
{
    public class BossService
    {
        private readonly string _userId;
        public BossService(string userId)
        {
            _userId = userId;
        }

        public bool CreateBoss(BossCreate model)
        {
                var entity =
                    new Boss()
                    {
                        UserId = _userId,
                        Name = model.Name,
                        Damage = model.Damage,
                    };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bosses.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BossListItem> GetBosses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bosses
                        .Select(
                            boss =>
                                new BossListItem
                                {
                                    Name = boss.Name,
                                    Damage = boss.Damage,
                                    DropTable = boss.DropTable
                                });
                return query.ToArray();
            }
        }

        public BossDetail GetBossById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bosses
                        .SingleOrDefault(e => e.BossId == id && e.UserId == _userId);
                return
                        new BossDetail
                        {
                            BossId = entity.BossId,
                            Name = entity.Name,
                            Damage = entity.Damage,
                            DropTable = entity.DropTable
                        };

            }
        }

        public bool UpdateItem(BossEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bosses
                        .Single(e => e.BossId == model.BossId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.Damage = model.Damage;
                entity.DropTable = model.DropTable;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBoss(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bosses
                        .Single(item => item.BossId == id && _userId == item.UserId);

                ctx.Bosses.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
