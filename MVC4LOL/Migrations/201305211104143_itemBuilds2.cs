namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemBuilds2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Item", "Item_Id", "dbo.Item");
            DropIndex("dbo.Item", new[] { "Item_Id" });
            CreateTable(
                "dbo.ItemRecipe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        IngreadientId = c.Int(nullable: false),
                        RecipeCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ingredient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.Ingredient_Id)
                .Index(t => t.ItemId)
                .Index(t => t.Ingredient_Id);
            
            DropColumn("dbo.Item", "Item_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "Item_Id", c => c.Int());
            DropIndex("dbo.ItemRecipe", new[] { "Ingredient_Id" });
            DropIndex("dbo.ItemRecipe", new[] { "ItemId" });
            DropForeignKey("dbo.ItemRecipe", "Ingredient_Id", "dbo.Item");
            DropForeignKey("dbo.ItemRecipe", "ItemId", "dbo.Item");
            DropTable("dbo.ItemRecipe");
            CreateIndex("dbo.Item", "Item_Id");
            AddForeignKey("dbo.Item", "Item_Id", "dbo.Item", "Id");
        }
    }
}
