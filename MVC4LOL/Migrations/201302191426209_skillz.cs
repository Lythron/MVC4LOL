namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skillz : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skill", "ChampionId", "dbo.Champion");
            DropForeignKey("dbo.Skill", "ChampionData_Id", "dbo.ChampionData");
            DropIndex("dbo.Skill", new[] { "ChampionId" });
            DropIndex("dbo.Skill", new[] { "ChampionData_Id" });
            AddColumn("dbo.Skill", "ChampionDataId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Skill", "ChampionDataId", "dbo.ChampionData", "Id", cascadeDelete: true);
            CreateIndex("dbo.Skill", "ChampionDataId");
            DropColumn("dbo.Skill", "ChampionId");
            DropColumn("dbo.Skill", "ChampionData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skill", "ChampionData_Id", c => c.Int());
            AddColumn("dbo.Skill", "ChampionId", c => c.Int(nullable: false));
            DropIndex("dbo.Skill", new[] { "ChampionDataId" });
            DropForeignKey("dbo.Skill", "ChampionDataId", "dbo.ChampionData");
            DropColumn("dbo.Skill", "ChampionDataId");
            CreateIndex("dbo.Skill", "ChampionData_Id");
            CreateIndex("dbo.Skill", "ChampionId");
            AddForeignKey("dbo.Skill", "ChampionData_Id", "dbo.ChampionData", "Id");
            AddForeignKey("dbo.Skill", "ChampionId", "dbo.Champion", "Id", cascadeDelete: true);
        }
    }
}
