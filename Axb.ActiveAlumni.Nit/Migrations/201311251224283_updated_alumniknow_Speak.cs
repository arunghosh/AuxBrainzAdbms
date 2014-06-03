namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_alumniknow_Speak : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlumniToKnow", "UserId", "dbo.User");
            DropIndex("dbo.AlumniToKnow", new[] { "UserId" });
            CreateTable(
                "dbo.AlumniSpeak",
                c => new
                    {
                        AlumniToKnowId = c.Int(nullable: false, identity: true),
                        AlumniId = c.Int(nullable: false),
                        Course = c.String(maxLength: 124),
                        Batch = c.Int(nullable: false),
                        AlumniName = c.String(maxLength: 128),
                        About = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlumniToKnowId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.AlumniToKnow");
        }
        
        public override void Down()
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
                        IsDeleted = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlumniToKnowId);
            
            DropIndex("dbo.AlumniSpeak", new[] { "UserId" });
            DropForeignKey("dbo.AlumniSpeak", "UserId", "dbo.User");
            DropTable("dbo.AlumniSpeak");
            CreateIndex("dbo.AlumniToKnow", "UserId");
            AddForeignKey("dbo.AlumniToKnow", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
