namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Accepted_Column_Contact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactModels", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactModels", "Accepted");
        }
    }
}
