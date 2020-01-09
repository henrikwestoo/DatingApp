namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile_active : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileModels", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileModels", "Active");
        }
    }
}
