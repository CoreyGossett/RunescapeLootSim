using RunescapeLootSim.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Models
{
    public class BossCreate
    {
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ICollection<Item> DropTable { get; set; }
        [Required]
        public int Damage { get; set; }
    }
}
