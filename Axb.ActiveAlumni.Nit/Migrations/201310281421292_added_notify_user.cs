namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_notify_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "MessageViewedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "MessageViewedAt");
        }
    }
}
