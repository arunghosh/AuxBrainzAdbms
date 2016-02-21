namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_discussion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discussion",
                c => new
                    {
                        DiscussionId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DiscussionId);
            
            CreateTable(
                "dbo.DiscussionUserMap",
                c => new
                    {
                        DiscussionUserMapId = c.Int(nullable: false, identity: true),
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.DiscussionUserMapId)
                .ForeignKey("dbo.Discussion", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId);
            
            CreateTable(
                "dbo.DiscussionComment",
                c => new
                    {
                        DiscussionCommentId = c.Int(nullable: false, identity: true),
                        DiscussionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        Text = c.String(maxLength: 512),
                        Date = c.String(),
                        Agree = c.Int(nullable: false),
                        Disagree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiscussionCommentId)
                .ForeignKey("dbo.Discussion", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DiscussionComment", new[] { "DiscussionId" });
            DropIndex("dbo.DiscussionUserMap", new[] { "DiscussionId" });
            DropForeignKey("dbo.DiscussionComment", "DiscussionId", "dbo.Discussion");
            DropForeignKey("dbo.DiscussionUserMap", "DiscussionId", "dbo.Discussion");
            DropTable("dbo.DiscussionComment");
            DropTable("dbo.DiscussionUserMap");
            DropTable("dbo.Discussion");
        }
    }
}
