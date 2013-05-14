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
    public class ChampionData : BaseStats
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
        
        //public Decimal Health { get; set; }
        public Decimal HealthPerLvl { get; set; }
        //public Decimal HealthRegen { get; set; }
        public Decimal HealthRegenPerLvl { get; set; }
        //public Decimal Mana { get; set; }
        public Decimal ManaPerLvl { get; set; }
        //public Decimal ManaRegen { get; set; }
        public Decimal ManaRegenPerLvl { get; set; }
        public Decimal Range { get; set; }
        //public Decimal Damage { get; set; }
        public Decimal DamagePerLvl { get; set; }
        
        //public Decimal AttackSpeed { get; set; } // TODO : delete
        public Decimal BaseAttackSpeed { get; set; }
        //public Decimal AttackSpeedPerLvl { get; set; } // TODO: delete
        public Decimal AttackSpeedPerLvl { get; set; }
        
        //public Decimal Armor { get; set; }
        public Decimal ArmorPerLvl { get; set; }
        //public Decimal MagicResist { get; set; }
        public Decimal MagicResistPerLvl { get; set; }
        //public Decimal MoveSpeed { get; set; }

        //public Decimal CriticalChance { get; set; }
        //public Decimal CriticalDamage { get; set; }
        //public Decimal FlatArmorPen { get; set; }
        //public Decimal PercentageArmorPen { get; set; }
        //public Decimal FlatMagicPen { get; set; }
        //public Decimal PercentageMagicPen { get; set; }
        //public Decimal LifeSteeling { get; set; }
        //public Decimal SpellVamp { get; set; }
        //public Decimal PercentageHealthBonus { get; set; }
        //public Decimal PercentageHealingBonus { get; set; }
        //public Decimal PercentageMoveBonus { get; set; }
        //public Decimal AbilityPower { get; set; }
        // Moved to base stats in order to not repeating it for items.

        //public virtual List<Skill> Skills { get; set; }

        public Int32 PatchVersionId { get; set; }
        public virtual PatchVersion PatchVersion { get; set; }

    }
}
