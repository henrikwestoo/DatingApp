namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Posts_DbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        RecieverId = c.Int(nullable: false),
                        Content = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileModels", t => t.CreatorId)
                .ForeignKey("dbo.ProfileModels", t => t.RecieverId)
                .Index(t => t.CreatorId)
                .Index(t => t.RecieverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "RecieverId", "dbo.ProfileModels");
            DropForeignKey("dbo.PostModels", "CreatorId", "dbo.ProfileModels");
            DropIndex("dbo.PostModels", new[] { "RecieverId" });
            DropIndex("dbo.PostModels", new[] { "CreatorId" });
            DropTable("dbo.PostModels");
        }
    }
}
