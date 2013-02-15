namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class championclean : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChampionData", "AtkSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
            DropColumn("dbo.Champion", "Title");
            DropColumn("dbo.Champion", "Image");
            DropColumn("dbo.Champion", "ReleaseDate");
            DropColumn("dbo.Champion", "IP_Cost");
            DropColumn("dbo.Champion", "RP_Cost");
            DropColumn("dbo.Champion", "Health");
            DropColumn("dbo.Champion", "HealthPerLvl");
            DropColumn("dbo.Champion", "HealthRegen");
            DropColumn("dbo.Champion", "HealthRegenPerLvl");
            DropColumn("dbo.Champion", "Mana");
            DropColumn("dbo.Champion", "ManaPerLvl");
            DropColumn("dbo.Champion", "ManaRegen");
            DropColumn("dbo.Champion", "ManaRegenPerLvl");
            DropColumn("dbo.Champion", "Range");
            DropColumn("dbo.Champion", "Damage");
            DropColumn("dbo.Champion", "DamagePerLvl");
            DropColumn("dbo.Champion", "AtkSpeed");
            DropColumn("dbo.Champion", "AtkSpeedPerLvl");
            DropColumn("dbo.Champion", "Armor");
            DropColumn("dbo.Champion", "ArmorPerLvl");
            DropColumn("dbo.Champion", "MagicResist");
            DropColumn("dbo.Champion", "MagicResistPerLvl");
            DropColumn("dbo.Champion", "MoveSpeed");
            DropColumn("dbo.Champion", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Champion", "Version", c => c.String());
            AddColumn("dbo.Champion", "MoveSpeed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "MagicResistPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "MagicResist", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "ArmorPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "Armor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "AtkSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "AtkSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
            AddColumn("dbo.Champion", "DamagePerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "Damage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "Range", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "ManaRegenPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "ManaRegen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "ManaPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "Mana", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "HealthRegenPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "HealthRegen", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "HealthPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "Health", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Champion", "RP_Cost", c => c.Int(nullable: false));
            AddColumn("dbo.Champion", "IP_Cost", c => c.Int(nullable: false));
            AddColumn("dbo.Champion", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Champion", "Image", c => c.Binary());
            AddColumn("dbo.Champion", "Title", c => c.String());
            AlterColumn("dbo.ChampionData", "AtkSpeed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
