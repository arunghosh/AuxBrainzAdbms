namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userrole_tble : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MapUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.MapUserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.MapUserRole", new[] { "UserId" });
            DropIndex("dbo.MapUserRole", new[] { "RoleId" });
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
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.User", "RoleCode");
            DropTable("dbo.Role");
            DropTable("dbo.MapUserRole");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MapUserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        Name = c.String(maxLength: 32),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            AddColumn("dbo.User", "RoleCode", c => c.Byte(nullable: false));
            DropIndex("dbo.UserResponsibility", new[] { "UserId" });
            DropForeignKey("dbo.UserResponsibility", "UserId", "dbo.User");
            DropTable("dbo.UserResponsibility");
            CreateIndex("dbo.MapUserRole", "RoleId");
            CreateIndex("dbo.MapUserRole", "UserId");
            AddForeignKey("dbo.MapUserRole", "RoleId", "dbo.Role", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.MapUserRole", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
