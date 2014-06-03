namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert_reltion_withUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relative", "RelativeUserId", "dbo.User");
            DropIndex("dbo.Relative", new[] { "RelativeUserId" });
            AddForeignKey("dbo.Relative", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Relative", "UserId");
            DropColumn("dbo.Relative", "UserName");
            DropColumn("dbo.Relative", "RelativeUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relative", "RelativeUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Relative", "UserName", c => c.String(maxLength: 128));
            DropIndex("dbo.Relative", new[] { "UserId" });
            DropForeignKey("dbo.Relative", "UserId", "dbo.User");
            CreateIndex("dbo.Relative", "RelativeUserId");
            AddForeignKey("dbo.Relative", "RelativeUserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
