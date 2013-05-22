namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemBuilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Item_Id", c => c.Int());
            AddForeignKey("dbo.Item", "Item_Id", "dbo.Item", "Id");
            CreateIndex("dbo.Item", "Item_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Item", new[] { "Item_Id" });
            DropForeignKey("dbo.Item", "Item_Id", "dbo.Item");
            DropColumn("dbo.Item", "Item_Id");
        }
    }
}
