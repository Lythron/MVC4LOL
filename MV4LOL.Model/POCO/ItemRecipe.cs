using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("ItemRecipe")]
    public class ItemRecipe
    {
        public Int32 Id { get; set; }

        public virtual Item Item { get; set; }

        public Int32 ItemId { get; set; }
        
        public virtual Item Component { get; set; }

        public Int32 ComponentId { get; set; }

        public Decimal RecipeCost { get; set; }
    }
}
