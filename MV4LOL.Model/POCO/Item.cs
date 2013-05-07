using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("Item")]
    public class Item : BaseStats
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Byte[] Image { get; set; }

        public String Description { get; set; }

        public String Availability { get; set; }

        public Int32 Cost { get; set; }
    }

    public abstract class BaseStats
    {
        public Decimal Health { get; set; }
        public Decimal HealthRegen { get; set; }
        public Decimal Mana { get; set; }
        public Decimal ManaRegen { get; set; }
        public Decimal Damage { get; set; }
        public Decimal AttackSpeed { get; set; }
        public Decimal Armor { get; set; }
        public Decimal MagicResist { get; set; }
        public Decimal MoveSpeed { get; set; }
    }
}
