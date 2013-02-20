namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patchversion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatchVersion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("Insert into PatchVersion values (GETDATE(), 'preseason 3')");
            
            AddColumn("dbo.ChampionData", "PatchVersionId", c => c.Int(nullable: false, defaultValue: 1));
            AddForeignKey("dbo.ChampionData", "PatchVersionId", "dbo.PatchVersion", "Id", cascadeDelete: true);
            CreateIndex("dbo.ChampionData", "PatchVersionId");
            DropColumn("dbo.ChampionData", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChampionData", "Version", c => c.String());
            DropIndex("dbo.ChampionData", new[] { "PatchVersionId" });
            DropForeignKey("dbo.ChampionData", "PatchVersionId", "dbo.PatchVersion");
            DropColumn("dbo.ChampionData", "PatchVersionId");
            DropTable("dbo.PatchVersion");
        }
    }
}
