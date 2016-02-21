namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_affnity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentAffinity",
                c => new
                    {
                        CommentAffinityId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        DiscussionCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentAffinityId)
                .ForeignKey("dbo.DiscussionComment", t => t.DiscussionCommentId, cascadeDelete: true)
                .Index(t => t.DiscussionCommentId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CommentAffinity", new[] { "DiscussionCommentId" });
            DropForeignKey("dbo.CommentAffinity", "DiscussionCommentId", "dbo.DiscussionComment");
            DropTable("dbo.CommentAffinity");
        }
    }
}
