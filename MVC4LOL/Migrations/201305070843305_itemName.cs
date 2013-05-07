namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Name");
        }
    }
}
