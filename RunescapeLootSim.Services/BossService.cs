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
        public BossService(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }

        public bool CreateBoss(BossCreate model)
        {
            var entity =
                new Boss()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Damage = model.Damage,
                    DropTable = model.DropTable
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

        public IEnumerable<BossListItem> GetBossesByUser(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bosses
                        .Where(boss => userId == boss.UserId)
                        .Select(
                            boss =>
                                new BossListItem
                                {
                                    BossId = boss.BossId,
                                    Name = boss.Name,
                                    Damage = boss.Damage,
                                    DropTable = boss.DropTable
                                });

                return query.ToArray();
            }
        }

        public bool UpdateBoss(BossEdit model, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bosses
                        .Single(e => e.BossId == model.BossId && userId == model.UserId);

                entity.Name = model.Name;
                entity.Damage = model.Damage;
                entity.DropTable = model.DropTable;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBoss(int bossId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bosses
                        .Single(boss => boss.BossId == bossId && userId == boss.UserId);

                ctx.Bosses.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
