namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contacts_Edit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileModels", t => t.ContactId)
                .ForeignKey("dbo.ProfileModels", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactModels", "ProfileId", "dbo.ProfileModels");
            DropForeignKey("dbo.ContactModels", "ContactId", "dbo.ProfileModels");
            DropIndex("dbo.ContactModels", new[] { "ContactId" });
            DropIndex("dbo.ContactModels", new[] { "ProfileId" });
            DropTable("dbo.ContactModels");
        }
    }
}
