namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_session : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.UserSessionId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Log", "IPAddress", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserSession", new[] { "UserId" });
            DropForeignKey("dbo.UserSession", "UserId", "dbo.User");
            DropColumn("dbo.Log", "IPAddress");
            DropTable("dbo.UserSession");
        }
    }
}
