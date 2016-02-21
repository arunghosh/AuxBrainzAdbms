namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_alumnitoKnow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlumniToKnow",
                c => new
                    {
                        AlumniToKnowId = c.Int(nullable: false, identity: true),
                        AlumniId = c.Int(nullable: false),
                        AlumniName = c.String(maxLength: 128),
                        About = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlumniToKnowId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AlumniToKnow", new[] { "UserId" });
            DropForeignKey("dbo.AlumniToKnow", "UserId", "dbo.User");
            DropTable("dbo.AlumniToKnow");
        }
    }
}
