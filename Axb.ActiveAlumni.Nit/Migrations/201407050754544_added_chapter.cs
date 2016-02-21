namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_chapter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapter",
                c => new
                    {
                        ChapterId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Alias = c.String(maxLength: 1024),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ChapterId);
            
            CreateTable(
                "dbo.ChapterHead",
                c => new
                    {
                        ChapterHeadId = c.Int(nullable: false, identity: true),
                        Position = c.String(maxLength: 32),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        Batch = c.String(maxLength: 10),
                        Branch = c.String(maxLength: 64),
                        ChapterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChapterHeadId)
                .ForeignKey("dbo.Chapter", t => t.ChapterId, cascadeDelete: true)
                .Index(t => t.ChapterId);
            
            AddColumn("dbo.User", "Chapter_ChapterId", c => c.Int());
            AddColumn("dbo.Event", "Chapter_ChapterId", c => c.Int());
            AddForeignKey("dbo.User", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
            AddForeignKey("dbo.Event", "Chapter_ChapterId", "dbo.Chapter", "ChapterId");
            CreateIndex("dbo.User", "Chapter_ChapterId");
            CreateIndex("dbo.Event", "Chapter_ChapterId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ChapterHead", new[] { "ChapterId" });
            DropIndex("dbo.Event", new[] { "Chapter_ChapterId" });
            DropIndex("dbo.User", new[] { "Chapter_ChapterId" });
            DropForeignKey("dbo.ChapterHead", "ChapterId", "dbo.Chapter");
            DropForeignKey("dbo.Event", "Chapter_ChapterId", "dbo.Chapter");
            DropForeignKey("dbo.User", "Chapter_ChapterId", "dbo.Chapter");
            DropColumn("dbo.Event", "Chapter_ChapterId");
            DropColumn("dbo.User", "Chapter_ChapterId");
            DropTable("dbo.ChapterHead");
            DropTable("dbo.Chapter");
        }
    }
}
