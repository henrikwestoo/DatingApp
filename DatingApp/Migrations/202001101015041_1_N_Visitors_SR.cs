namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1_N_Visitors_SR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileModels", "VisitorId", c => c.Int());
            CreateIndex("dbo.ProfileModels", "VisitorId");
            AddForeignKey("dbo.ProfileModels", "VisitorId", "dbo.ProfileModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileModels", "VisitorId", "dbo.ProfileModels");
            DropIndex("dbo.ProfileModels", new[] { "VisitorId" });
            DropColumn("dbo.ProfileModels", "VisitorId");
        }
    }
}
