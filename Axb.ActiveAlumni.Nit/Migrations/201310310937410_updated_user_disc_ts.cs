namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_disc_ts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "TsMessageView", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "TsDiscussionView", c => c.DateTime(nullable: false));
            DropColumn("dbo.User", "MessageViewedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "MessageViewedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.User", "TsDiscussionView");
            DropColumn("dbo.User", "TsMessageView");
        }
    }
}
