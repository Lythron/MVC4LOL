namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class builds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Health = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HealthRegen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mana = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManaRegen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Damage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AttackSpeed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Armor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MagicResist = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoveSpeed = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Build",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChampionDataId = c.Int(nullable: false),
                        RunePageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChampionData", t => t.ChampionDataId, cascadeDelete: true)
                .ForeignKey("dbo.RunePages", t => t.RunePageId, cascadeDelete: true)
                .Index(t => t.ChampionDataId)
                .Index(t => t.RunePageId);
            
            CreateTable(
                "dbo.RunePages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BuildItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Runes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RuneTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RuneTypes", t => t.RuneTypeId, cascadeDelete: true)
                .Index(t => t.RuneTypeId);
            
            CreateTable(
                "dbo.RuneTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RuneForRunePages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RunePageId = c.Int(nullable: false),
                        RuneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RunePages", t => t.RunePageId, cascadeDelete: true)
                .ForeignKey("dbo.Runes", t => t.RuneId, cascadeDelete: true)
                .Index(t => t.RunePageId)
                .Index(t => t.RuneId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RuneForRunePages", new[] { "RuneId" });
            DropIndex("dbo.RuneForRunePages", new[] { "RunePageId" });
            DropIndex("dbo.Runes", new[] { "RuneTypeId" });
            DropIndex("dbo.BuildItems", new[] { "ItemId" });
            DropIndex("dbo.Build", new[] { "RunePageId" });
            DropIndex("dbo.Build", new[] { "ChampionDataId" });
            DropForeignKey("dbo.RuneForRunePages", "RuneId", "dbo.Runes");
            DropForeignKey("dbo.RuneForRunePages", "RunePageId", "dbo.RunePages");
            DropForeignKey("dbo.Runes", "RuneTypeId", "dbo.RuneTypes");
            DropForeignKey("dbo.BuildItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Build", "RunePageId", "dbo.RunePages");
            DropForeignKey("dbo.Build", "ChampionDataId", "dbo.ChampionData");
            DropTable("dbo.RuneForRunePages");
            DropTable("dbo.RuneTypes");
            DropTable("dbo.Runes");
            DropTable("dbo.BuildItems");
            DropTable("dbo.RunePages");
            DropTable("dbo.Build");
            DropTable("dbo.Items");
        }
    }
}
