namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class common_threadIte : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscussionComment", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.DiscussionComment", "SenderName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.DiscussionComment", "Text", c => c.String(nullable: false, maxLength: 512));
            DropColumn("dbo.DiscussionComment", "UserId");
            DropColumn("dbo.DiscussionComment", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiscussionComment", "UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.DiscussionComment", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.DiscussionComment", "Text", c => c.String(maxLength: 512));
            DropColumn("dbo.DiscussionComment", "SenderName");
            DropColumn("dbo.DiscussionComment", "SenderId");
        }
    }
}
