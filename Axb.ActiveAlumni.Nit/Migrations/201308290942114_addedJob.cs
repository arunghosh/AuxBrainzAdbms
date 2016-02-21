namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 128),
                        Location = c.String(maxLength: 128),
                        Position = c.String(maxLength: 64),
                        Domain = c.String(maxLength: 64),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Job", new[] { "UserId" });
            DropForeignKey("dbo.Job", "UserId", "dbo.User");
            DropTable("dbo.Job");
        }
    }
}
