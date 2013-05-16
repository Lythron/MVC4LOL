using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC4LOL.Model
{
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
        public Decimal CriticalChance { get; set; }
        public Decimal CriticalDamage { get; set; }
        public Decimal FlatArmorPen { get; set; }
        public Decimal PercentageArmorPen { get; set; }
        public Decimal FlatMagicPen { get; set; }
        public Decimal PercentageMagicPen { get; set; }
        public Decimal LifeSteal { get; set; }
        public Decimal SpellVamp { get; set; }
        public Decimal PercentageHealthBonus { get; set; }
        public Decimal PercentageHealingBonus { get; set; }
        public Decimal PercentageMoveBonus { get; set; }
        public Decimal AbilityPower { get; set; }
        public Decimal CooldownReduction { get; set; }
    }
}
