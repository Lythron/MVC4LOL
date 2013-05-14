namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cooldownReduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionData", "CooldownReduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Item", "CooldownReduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "CooldownReduction");
            DropColumn("dbo.ChampionData", "CooldownReduction");
        }
    }
}
