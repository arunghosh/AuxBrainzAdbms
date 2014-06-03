namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLog",
                c => new
                    {
                        UserLogId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 512),
                        ByUserId = c.Int(nullable: false),
                        ByUserName = c.String(maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserLogId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserLog", new[] { "UserId" });
            DropForeignKey("dbo.UserLog", "UserId", "dbo.User");
            DropTable("dbo.UserLog");
        }
    }
}
