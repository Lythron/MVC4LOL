using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("Skill")]
    public class Skill
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 Lvl { get; set; }
        public String Description { get; set; }

        public Int32 ChampionDataId { get; set; }
        public virtual ChampionData ChampionData { get; set; }
    }
}
