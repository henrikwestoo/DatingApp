namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile_FK_Update : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProfileModels", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.ProfileModels", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProfileModels", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.ProfileModels", name: "UserId", newName: "User_Id");
        }
    }
}
