namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixedtypos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PostModels", name: "RecieverId", newName: "ReceiverId");
            RenameIndex(table: "dbo.PostModels", name: "IX_RecieverId", newName: "IX_ReceiverId");
            AddColumn("dbo.ProfileModels", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.ProfileModels", "_Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProfileModels", "_Gender", c => c.Int(nullable: false));
            DropColumn("dbo.ProfileModels", "Gender");
            RenameIndex(table: "dbo.PostModels", name: "IX_ReceiverId", newName: "IX_RecieverId");
            RenameColumn(table: "dbo.PostModels", name: "ReceiverId", newName: "RecieverId");
        }
    }
}
