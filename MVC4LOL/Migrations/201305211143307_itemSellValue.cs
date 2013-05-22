namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemSellValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "SellValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "SellValue");
        }
    }
}
