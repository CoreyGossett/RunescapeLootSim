using RunescapeLootSim.Data;
using RunescapeLootSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Services
{
    public class KillService
    {
        public bool CreateKill(KillCreate model)
        {
            var entity =
                new Kill()
                {
                    UserId = model.UserId,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Kills.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<KillListItem> GetKillsByUser(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Kills
                        .Where(kill => userId == kill.UserId)
                        .Select(
                            kill =>
                                new KillListItem
                                {
                                    UserId = kill.UserId,
                                    BossId = kill.BossId
                                });
                return query.ToArray();
            }
        }

        public bool UpdateKill(KillEdit model, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kills
                        .Single(e => e.KillId == model.KillId && userId == model.UserId);

                entity.BossId = model.BossId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteKill(int killId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kills
                        .Single(kill => kill.KillId == killId && userId == kill.UserId);

                ctx.Kills.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
