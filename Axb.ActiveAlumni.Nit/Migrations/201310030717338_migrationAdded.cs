namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MentorShip",
                c => new
                    {
                        MentorShipId = c.Int(nullable: false, identity: true),
                        AlumniId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MentorShipId);
            
            CreateTable(
                "dbo.MentorMessage",
                c => new
                    {
                        MentorMessageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(maxLength: 512),
                        Status = c.Int(nullable: false),
                        MentorShipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MentorMessageId)
                .ForeignKey("dbo.MentorShip", t => t.MentorShipId, cascadeDelete: true)
                .Index(t => t.MentorShipId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MentorMessage", new[] { "MentorShipId" });
            DropForeignKey("dbo.MentorMessage", "MentorShipId", "dbo.MentorShip");
            DropTable("dbo.MentorMessage");
            DropTable("dbo.MentorShip");
        }
    }
}
