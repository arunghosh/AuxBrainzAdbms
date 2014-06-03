namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_session_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSession", "UserId", "dbo.User");
            DropIndex("dbo.UserSession", new[] { "UserId" });
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        UserSessionId = c.Int(nullable: false, identity: true),
                        SessionId = c.String(maxLength: 32),
                        IPAddress = c.String(maxLength: 32),
                        Browser = c.String(maxLength: 64),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSessionId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.UserSession");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserSession",
                c => new
                    {
                        UserSessionId = c.Int(nullable: false, identity: true),
                        SessionId = c.String(maxLength: 32),
                        IPAddress = c.String(maxLength: 32),
                        Browser = c.String(maxLength: 64),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSessionId);
            
            DropIndex("dbo.Session", new[] { "UserId" });
            DropForeignKey("dbo.Session", "UserId", "dbo.User");
            DropTable("dbo.Session");
            CreateIndex("dbo.UserSession", "UserId");
            AddForeignKey("dbo.UserSession", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
