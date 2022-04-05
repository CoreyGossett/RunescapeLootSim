using RunescapeLootSim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Models
{
    public class ItemListItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public decimal DropRate { get; set; }
    }
}
