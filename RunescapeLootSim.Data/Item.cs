using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Data
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Damage { get; set; }
        [Required]
        public decimal DropRate { get; set; }
    }
}
