using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Data
{
    public class Boss
    {
        [Key]
        public int BossId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ICollection<Item> DropTable { get; set; }
        [Required]
        public int Damage { get; set; }
    }
}
