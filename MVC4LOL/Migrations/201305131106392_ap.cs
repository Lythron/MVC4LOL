namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionData", "AbilityPower", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChampionData", "AbilityPower");
        }
    }
}
