namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_categories_contacts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactModels", "ProfileCategory", c => c.Int(nullable: false));
            AddColumn("dbo.ContactModels", "ContactCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactModels", "ContactCategory");
            DropColumn("dbo.ContactModels", "ProfileCategory");
        }
    }
}
