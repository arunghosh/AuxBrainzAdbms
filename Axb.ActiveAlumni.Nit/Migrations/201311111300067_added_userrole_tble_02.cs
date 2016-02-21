namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userrole_tble_02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.UserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropTable("dbo.UserRole");
            CreateIndex("dbo.UserRoles", "UserId");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
