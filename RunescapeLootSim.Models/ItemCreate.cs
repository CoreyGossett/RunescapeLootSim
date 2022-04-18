using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunescapeLootSim.Models
{
    public class ItemCreate
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public int Damage { get; set; }
        [Required]
        public decimal DropRate { get; set; }
    }   
}
