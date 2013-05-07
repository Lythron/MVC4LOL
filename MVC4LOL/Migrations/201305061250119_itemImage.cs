namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Image");
        }
    }
}
