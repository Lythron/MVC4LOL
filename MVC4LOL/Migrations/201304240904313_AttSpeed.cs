namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttSpeed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChampionData", "AtkSpeed", c => c.Decimal(nullable: false, precision: 8, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChampionData", "AtkSpeed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
