namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChampionExtendedProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionData", "AttackSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
            AddColumn("dbo.ChampionData", "AttackSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "CriticalChance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "CriticalDamage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "FlatArmorPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "PercentageArmorPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "FlatMagicPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "PercentageMagicPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "LifeSteeling", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "SpellVamp", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "PercentageHealthBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "PercentageHealingBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChampionData", "PercentageHealingBonus");
            DropColumn("dbo.ChampionData", "PercentageHealthBonus");
            DropColumn("dbo.ChampionData", "SpellVamp");
            DropColumn("dbo.ChampionData", "LifeSteeling");
            DropColumn("dbo.ChampionData", "PercentageMagicPen");
            DropColumn("dbo.ChampionData", "FlatMagicPen");
            DropColumn("dbo.ChampionData", "PercentageArmorPen");
            DropColumn("dbo.ChampionData", "FlatArmorPen");
            DropColumn("dbo.ChampionData", "CriticalDamage");
            DropColumn("dbo.ChampionData", "CriticalChance");
            DropColumn("dbo.ChampionData", "AttackSpeedPerLvl");
            DropColumn("dbo.ChampionData", "AttackSpeed");
        }
    }
}
