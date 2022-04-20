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
        private readonly string _userId;
        public KillService(string userId)
        {
            _userId = userId;
        }

        public bool CreateKill(KillCreate model)
        {
            var entity =
                new Kill()
                {
                    UserId = model.UserId,
                    BossId = model.BossId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Kills.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public KillDetail GetKillById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kills
                        .SingleOrDefault(e => e.KillId == id && e.UserId == _userId);
                return
                        new KillDetail
                        {
                            KillId = entity.KillId,
                        };

            }
        }

        public IEnumerable<KillListItem> GetKills()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Kills
                        .Select(
                            kill =>
                                new KillListItem
                                {
                                    KillId = kill.KillId
                                });
                return query.ToArray();
            }
        }

        public bool DeleteKill(int killId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Kills
                        .Single(item => item.KillId == killId && _userId == item.UserId);

                ctx.Kills.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
