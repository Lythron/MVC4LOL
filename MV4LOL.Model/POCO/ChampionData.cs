using MVC4LOL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
    [Table("ChampionData")]
    public class ChampionData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        public Int32 ChampionId { get; set; }
        public virtual Champion Champion { get; set; }

        public String Name { get; set; }
        public String Title { get; set; }
        public Byte[] Image { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public Int32 IP_Cost { get; set; }
        public Int32 RP_Cost { get; set; }
        
        public Decimal Health { get; set; }
        public Decimal HealthPerLvl { get; set; }
        public Decimal HealthRegen { get; set; }
        public Decimal HealthRegenPerLvl { get; set; }
        public Decimal Mana { get; set; }
        public Decimal ManaPerLvl { get; set; }
        public Decimal ManaRegen { get; set; }
        public Decimal ManaRegenPerLvl { get; set; }
        public Decimal Range { get; set; }
        public Decimal Damage { get; set; }
        public Decimal DamagePerLvl { get; set; }
        public Decimal AtkSpeed { get; set; }
        public Decimal AtkSpeedPerLvl { get; set; }
        public Decimal Armor { get; set; }
        public Decimal ArmorPerLvl { get; set; }
        public Decimal MagicResist { get; set; }
        public Decimal MagicResistPerLvl { get; set; }
        public Decimal MoveSpeed { get; set; }

        public virtual List<Skill> Skills { get; set; }

        public String Version { get; set; }
    }
}
