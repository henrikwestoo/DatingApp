namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProfileModels", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProfileModels", "Image", c => c.Binary());
        }
    }
}
