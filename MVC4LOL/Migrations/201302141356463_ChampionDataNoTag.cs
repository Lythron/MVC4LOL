namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChampionDataNoTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tag", "ChampionData_Id", "dbo.ChampionData");
            DropIndex("dbo.Tag", new[] { "ChampionData_Id" });
            DropColumn("dbo.Tag", "ChampionData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tag", "ChampionData_Id", c => c.Int());
            CreateIndex("dbo.Tag", "ChampionData_Id");
            AddForeignKey("dbo.Tag", "ChampionData_Id", "dbo.ChampionData", "Id");
        }
    }
}
