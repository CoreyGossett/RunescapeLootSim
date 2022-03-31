using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Data
{
    public class Kill
    {
        [Key]
        public int KillId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Boss")]
        public int BossId { get; set; }
        public virtual Boss Boss { get; set; }
    }
}
