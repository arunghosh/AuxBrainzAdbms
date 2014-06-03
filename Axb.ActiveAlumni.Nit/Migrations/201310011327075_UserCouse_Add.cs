namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCouse_Add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "BranchId", "dbo.Branch");
            DropIndex("dbo.User", new[] { "BranchId" });
            CreateTable(
                "dbo.UserCourse",
                c => new
                    {
                        UserCourseId = c.Int(nullable: false, identity: true),
                        Batch = c.String(),
                        BranchId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserCourseId)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.User", "Branch_BranchId", c => c.Int());
            AddForeignKey("dbo.User", "Branch_BranchId", "dbo.Branch", "BranchId");
            CreateIndex("dbo.User", "Branch_BranchId");
            DropColumn("dbo.User", "BranchId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "BranchId", c => c.Int(nullable: false));
            DropIndex("dbo.UserCourse", new[] { "UserId" });
            DropIndex("dbo.UserCourse", new[] { "BranchId" });
            DropIndex("dbo.User", new[] { "Branch_BranchId" });
            DropForeignKey("dbo.UserCourse", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCourse", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.User", "Branch_BranchId", "dbo.Branch");
            DropColumn("dbo.User", "Branch_BranchId");
            DropTable("dbo.UserCourse");
            CreateIndex("dbo.User", "BranchId");
            AddForeignKey("dbo.User", "BranchId", "dbo.Branch", "BranchId", cascadeDelete: true);
        }
    }
}
