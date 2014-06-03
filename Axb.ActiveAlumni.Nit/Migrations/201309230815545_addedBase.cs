namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Education", "UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Education", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Education", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Education", new[] { "UserId" });
            DropForeignKey("dbo.Education", "UserId", "dbo.User");
            DropColumn("dbo.Education", "UserId");
        }
    }
}
