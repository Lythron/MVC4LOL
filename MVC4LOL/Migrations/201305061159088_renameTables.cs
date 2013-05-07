namespace MVC4LOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Items", newName: "Item");
            RenameTable(name: "dbo.RunePages", newName: "RunePage");
            RenameTable(name: "dbo.BuildItems", newName: "BuildItem");
            RenameTable(name: "dbo.Runes", newName: "Rune");
            RenameTable(name: "dbo.RuneTypes", newName: "RuneType");
            RenameTable(name: "dbo.RuneForRunePages", newName: "RuneForRunePage");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RuneForRunePage", newName: "RuneForRunePages");
            RenameTable(name: "dbo.RuneType", newName: "RuneTypes");
            RenameTable(name: "dbo.Rune", newName: "Runes");
            RenameTable(name: "dbo.BuildItem", newName: "BuildItems");
            RenameTable(name: "dbo.RunePage", newName: "RunePages");
            RenameTable(name: "dbo.Item", newName: "Items");
        }
    }
}
