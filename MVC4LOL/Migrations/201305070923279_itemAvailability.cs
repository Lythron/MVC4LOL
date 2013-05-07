namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Availability", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Availability");
        }
    }
}
