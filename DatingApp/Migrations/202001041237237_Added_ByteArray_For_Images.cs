namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ByteArray_For_Images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileModels", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileModels", "Image");
        }
    }
}
