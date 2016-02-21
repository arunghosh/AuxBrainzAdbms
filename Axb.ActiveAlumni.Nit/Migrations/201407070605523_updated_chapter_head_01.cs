namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_chapter_head_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChapterHead", "Chapter_ChapterId", "dbo.Chapter");
            DropForeignKey("dbo.ChapterHead", "Chapter_ChapterId1", "dbo.Chapter");
            DropIndex("dbo.ChapterHead", new[] { "Chapter_ChapterId" });
            DropIndex("dbo.ChapterHead", new[] { "Chapter_ChapterId1" });
            DropColumn("dbo.ChapterHead", "Chapter_ChapterId");
            DropColumn("dbo.ChapterHead", "Chapter_ChapterId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChapterHead", "Chapter_ChapterId1", c => c.Int());
            AddColumn("dbo.ChapterHead", "Chapter_ChapterId", c => c.Int());
            CreateIndex("dbo.ChapterHead", "Chapter_ChapterId1");
            CreateIndex("dbo.ChapterHead", "Chapter_ChapterId");
            AddForeignKey("dbo.ChapterHead", "Chapter_ChapterId1", "dbo.Chapter", "ChapterId");
            AddForeignKey("dbo.ChapterHead", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
        }
    }
}
