namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_circle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Circle",
                c => new
                    {
                        CircleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CircleId);
            
            CreateTable(
                "dbo.MapCircleUser",
                c => new
                    {
                        CircleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CircleId, t.UserId })
                .ForeignKey("dbo.Circle", t => t.CircleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CircleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MapCircleUser", new[] { "UserId" });
            DropIndex("dbo.MapCircleUser", new[] { "CircleId" });
            DropForeignKey("dbo.MapCircleUser", "UserId", "dbo.User");
            DropForeignKey("dbo.MapCircleUser", "CircleId", "dbo.Circle");
            DropTable("dbo.MapCircleUser");
            DropTable("dbo.Circle");
        }
    }
}
