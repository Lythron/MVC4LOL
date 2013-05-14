namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basestats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionData", "BaseAttackSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
            AddColumn("dbo.ChampionData", "PercentageMoveBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "CriticalChance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "CriticalDamage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "FlatArmorPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "PercentageArmorPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "FlatMagicPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "PercentageMagicPen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "LifeSteeling", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "SpellVamp", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "PercentageHealthBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "PercentageHealingBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "PercentageMoveBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "AbilityPower", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "AbilityPower");
            DropColumn("dbo.Item", "PercentageMoveBonus");
            DropColumn("dbo.Item", "PercentageHealingBonus");
            DropColumn("dbo.Item", "PercentageHealthBonus");
            DropColumn("dbo.Item", "SpellVamp");
            DropColumn("dbo.Item", "LifeSteeling");
            DropColumn("dbo.Item", "PercentageMagicPen");
            DropColumn("dbo.Item", "FlatMagicPen");
            DropColumn("dbo.Item", "PercentageArmorPen");
            DropColumn("dbo.Item", "FlatArmorPen");
            DropColumn("dbo.Item", "CriticalDamage");
            DropColumn("dbo.Item", "CriticalChance");
            DropColumn("dbo.ChampionData", "PercentageMoveBonus");
            DropColumn("dbo.ChampionData", "BaseAttackSpeed");
        }
    }
}
