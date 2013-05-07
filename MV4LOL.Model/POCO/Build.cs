using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("Build")]
    public class Build
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public Int32 ChampionDataId { get; set; }
        public virtual ChampionData ChampionData { get; set; }
        //public Int32 MasteryId { get; set; }
        public Int32 RunePageId { get; set; }
        public virtual RunePage RunePage { get; set; }
    }

    [Table("BuildItem")]
    public class BuildItem
    {
        public Int32 Id { get; set; }
        public Int32 BuildId { get; set; }
        public Int32 ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
