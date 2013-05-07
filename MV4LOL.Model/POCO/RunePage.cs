using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("RunePage")]
    public class RunePage
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }

    [Table("Rune")]
    public class Rune
    {
        public Int32 Id { get; set; }
        public Int32 RuneTypeId { get; set; }
        public virtual RuneType RuneType { get; set; } 
    }

    [Table("RuneType")]
    public class RuneType
    {
        public Int32 Id { get; set; }
        public String Type { get; set; }
    }

    [Table("RuneForRunePage")]
    public class RuneForRunePage
    {
        public Int32 Id { get; set; }
        public Int32 RunePageId { get; set; }
        public virtual RunePage RunePage { get; set; }
        public Int32 RuneId { get; set; }
        public virtual Rune Rune { get; set; }
    }
}
