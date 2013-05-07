namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Cost", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Cost");
        }
    }
}
