using RunescapeLootSim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Models
{
    public class BossListItem
    {
        public int BossId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Item> DropTable { get; set; }
        public int Damage { get; set; }
    }
}
