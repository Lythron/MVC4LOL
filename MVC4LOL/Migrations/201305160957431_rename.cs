namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionData", "LifeSteal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "LifeSteal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ChampionData", "LifeSteeling");
            DropColumn("dbo.Item", "LifeSteeling");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "LifeSteeling", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChampionData", "LifeSteeling", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Item", "LifeSteal");
            DropColumn("dbo.ChampionData", "LifeSteal");
        }
    }
}
