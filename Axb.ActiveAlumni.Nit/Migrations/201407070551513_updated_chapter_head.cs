namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_chapter_head : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChapterHead", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChapterHead", "Chapter_ChapterId", c => c.Int());
            AddColumn("dbo.ChapterHead", "Chapter_ChapterId1", c => c.Int());
            AddForeignKey("dbo.ChapterHead", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
            AddForeignKey("dbo.ChapterHead", "Chapter_ChapterId1", "dbo.Chapter", "ChapterId");
            CreateIndex("dbo.ChapterHead", "Chapter_ChapterId");
            CreateIndex("dbo.ChapterHead", "Chapter_ChapterId1");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ChapterHead", new[] { "Chapter_ChapterId1" });
            DropIndex("dbo.ChapterHead", new[] { "Chapter_ChapterId" });
            DropForeignKey("dbo.ChapterHead", "Chapter_ChapterId1", "dbo.Chapter");
            DropForeignKey("dbo.ChapterHead", "Chapter_ChapterId", "dbo.Chapter");
            DropColumn("dbo.ChapterHead", "Chapter_ChapterId1");
            DropColumn("dbo.ChapterHead", "Chapter_ChapterId");
            DropColumn("dbo.ChapterHead", "IsDeleted");
        }
    }
}
