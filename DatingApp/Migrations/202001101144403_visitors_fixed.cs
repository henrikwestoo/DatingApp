namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visitors_fixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfileModelProfileModels", "ProfileModel_Id", "dbo.ProfileModels");
            DropForeignKey("dbo.ProfileModelProfileModels", "ProfileModel_Id1", "dbo.ProfileModels");
            DropIndex("dbo.ProfileModelProfileModels", new[] { "ProfileModel_Id" });
            DropIndex("dbo.ProfileModelProfileModels", new[] { "ProfileModel_Id1" });
            CreateTable(
                "dbo.VisitorModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        VisitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileModels", t => t.ProfileId)
                .ForeignKey("dbo.ProfileModels", t => t.VisitorId)
                .Index(t => t.ProfileId)
                .Index(t => t.VisitorId);
            
            DropTable("dbo.ProfileModelProfileModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfileModelProfileModels",
                c => new
                    {
                        ProfileModel_Id = c.Int(nullable: false),
                        ProfileModel_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileModel_Id, t.ProfileModel_Id1 });
            
            DropForeignKey("dbo.VisitorModels", "VisitorId", "dbo.ProfileModels");
            DropForeignKey("dbo.VisitorModels", "ProfileId", "dbo.ProfileModels");
            DropIndex("dbo.VisitorModels", new[] { "VisitorId" });
            DropIndex("dbo.VisitorModels", new[] { "ProfileId" });
            DropTable("dbo.VisitorModels");
            CreateIndex("dbo.ProfileModelProfileModels", "ProfileModel_Id1");
            CreateIndex("dbo.ProfileModelProfileModels", "ProfileModel_Id");
            AddForeignKey("dbo.ProfileModelProfileModels", "ProfileModel_Id1", "dbo.ProfileModels", "Id");
            AddForeignKey("dbo.ProfileModelProfileModels", "ProfileModel_Id", "dbo.ProfileModels", "Id");
        }
    }
}
