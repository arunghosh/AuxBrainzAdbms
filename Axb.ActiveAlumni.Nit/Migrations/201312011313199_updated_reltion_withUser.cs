namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_reltion_withUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relative", "UserId", "dbo.User");
            DropIndex("dbo.Relative", new[] { "UserId" });
            AddColumn("dbo.Relative", "UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Relative", "RelativeUserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Relative", "RelativeUserId", "dbo.User", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Relative", "RelativeUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Relative", new[] { "RelativeUserId" });
            DropForeignKey("dbo.Relative", "RelativeUserId", "dbo.User");
            DropColumn("dbo.Relative", "RelativeUserId");
            DropColumn("dbo.Relative", "UserName");
            CreateIndex("dbo.Relative", "UserId");
            AddForeignKey("dbo.Relative", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
