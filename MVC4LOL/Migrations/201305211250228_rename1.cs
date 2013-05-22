namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemRecipe", "Ingredient_Id", "dbo.Item");
            DropIndex("dbo.ItemRecipe", new[] { "Ingredient_Id" });
            AddColumn("dbo.ItemRecipe", "ComponentId", c => c.Int(nullable: false));
            AddForeignKey("dbo.ItemRecipe", "ComponentId", "dbo.Item", "Id", cascadeDelete: false);
            CreateIndex("dbo.ItemRecipe", "ComponentId");
            DropColumn("dbo.ItemRecipe", "IngreadientId");
            DropColumn("dbo.ItemRecipe", "Ingredient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemRecipe", "Ingredient_Id", c => c.Int());
            AddColumn("dbo.ItemRecipe", "IngreadientId", c => c.Int(nullable: false));
            DropIndex("dbo.ItemRecipe", new[] { "ComponentId" });
            DropForeignKey("dbo.ItemRecipe", "ComponentId", "dbo.Item");
            DropColumn("dbo.ItemRecipe", "ComponentId");
            CreateIndex("dbo.ItemRecipe", "Ingredient_Id");
            AddForeignKey("dbo.ItemRecipe", "Ingredient_Id", "dbo.Item", "Id");
        }
    }
}
