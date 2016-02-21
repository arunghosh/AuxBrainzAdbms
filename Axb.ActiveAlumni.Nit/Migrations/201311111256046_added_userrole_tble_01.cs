namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userrole_tble_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserResponsibility", "UserId", "dbo.User");
            DropIndex("dbo.UserResponsibility", new[] { "UserId" });
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false, defaultValue: 0),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.UserResponsibility");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserResponsibility",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropTable("dbo.UserRoles");
            CreateIndex("dbo.UserResponsibility", "UserId");
            AddForeignKey("dbo.UserResponsibility", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
