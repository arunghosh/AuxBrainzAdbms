namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_news_anonymos_02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AlumniNews", "UserId", "dbo.User");
            DropIndex("dbo.AlumniNews", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.AlumniNews", "UserId");
            AddForeignKey("dbo.AlumniNews", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
