namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_jobOpening : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobOpening",
                c => new
                    {
                        JobPostId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(maxLength: 128),
                        ExperienceFrom = c.Int(nullable: false),
                        ExperienceTo = c.Int(nullable: false),
                        Description = c.String(maxLength: 512),
                        PostedOn = c.DateTime(nullable: false),
                        SendYourResumesTo = c.String(),
                        Tags = c.String(maxLength: 124),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobPostId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.JobOpening", new[] { "UserId" });
            DropForeignKey("dbo.JobOpening", "UserId", "dbo.User");
            DropTable("dbo.JobOpening");
        }
    }
}
