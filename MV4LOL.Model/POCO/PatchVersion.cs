using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("PatchVersion")]
    public class PatchVersion
    {
        [Key]
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public String Name { get; set; }
        public String Notes { get; set; }
    }
}
