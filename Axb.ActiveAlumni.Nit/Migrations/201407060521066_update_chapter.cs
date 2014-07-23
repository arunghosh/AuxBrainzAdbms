namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_chapter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "Chapter_ChapterId", "dbo.Chapter");
            DropForeignKey("dbo.Event", "Chapter_ChapterId", "dbo.Chapter");
            DropIndex("dbo.User", new[] { "Chapter_ChapterId" });
            DropIndex("dbo.Event", new[] { "Chapter_ChapterId" });
            DropColumn("dbo.User", "Chapter_ChapterId");
            DropColumn("dbo.Event", "Chapter_ChapterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "Chapter_ChapterId", c => c.Int());
            AddColumn("dbo.User", "Chapter_ChapterId", c => c.Int());
            CreateIndex("dbo.Event", "Chapter_ChapterId");
            CreateIndex("dbo.User", "Chapter_ChapterId");
            AddForeignKey("dbo.Event", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
            AddForeignKey("dbo.User", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
        }
    }
}
