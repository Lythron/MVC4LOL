using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("Tag")]
    public class Tag
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public Int32 UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public Int32 ChampionId { get; set; }
        public virtual Champion Champion { get; set; }
    }
}
