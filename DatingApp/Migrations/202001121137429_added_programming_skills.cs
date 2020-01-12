namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_programming_skills : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileModels", "CSharp", c => c.Int(nullable: false));
            AddColumn("dbo.ProfileModels", "JavaScript", c => c.Int(nullable: false));
            AddColumn("dbo.ProfileModels", "StackOverflow", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileModels", "StackOverflow");
            DropColumn("dbo.ProfileModels", "JavaScript");
            DropColumn("dbo.ProfileModels", "CSharp");
        }
    }
}
