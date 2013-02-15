namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Champion", "AtkSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Champion", "AtsSpeedPerLvl");

            AddColumn("dbo.ChampionData", "AtkSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ChampionData", "AtsSpeedPerLvl");

            Sql(@"
                insert into ChampionData 
                (ChampionId, Name, Title, Image, ReleaseDate, IP_Cost, RP_Cost, Health, HealthPerLvl, HealthRegen, HealthRegenPerLvl, Mana, ManaPerLvl, ManaRegen, ManaRegenPerLvl, [Range], Damage, DamagePerLvl, AtkSpeed, AtkSpeedPerLvl, Armor, ArmorPerLvl, MagicResist, MagicResistPerLvl, MoveSpeed, [Version])
                select
                Id, Name, Title, Image, ReleaseDate, IP_Cost, RP_Cost, Health, HealthPerLvl, HealthRegen, HealthRegenPerLvl, Mana, ManaPerLvl, ManaRegen, ManaRegenPerLvl, [Range], Damage, DamagePerLvl, AtkSpeed, AtkSpeedPerLvl, Armor, ArmorPerLvl, MagicResist, MagicResistPerLvl, MoveSpeed, [Version]
                from champion;"
               );

        }
        
        public override void Down()
        {
            AddColumn("dbo.Champion", "AtsSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Champion", "AtkSpeedPerLvl");

            // ?
        }
    }
}
