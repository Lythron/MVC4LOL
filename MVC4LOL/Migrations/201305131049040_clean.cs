namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clean : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ChampionData", "AtkSpeed");
            DropColumn("dbo.ChampionData", "AtkSpeedPerLvl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChampionData", "AtkSpeedPerLvl", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "AtkSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
        }
    }
}
