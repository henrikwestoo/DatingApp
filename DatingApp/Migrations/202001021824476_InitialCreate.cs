namespace DatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        _Gender = c.Int(nullable: false),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProfileModels");
        }
    }
}
