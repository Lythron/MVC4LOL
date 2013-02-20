namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patchnotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatchVersion", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatchVersion", "Notes");
        }
    }
}
