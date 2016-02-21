namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_discCmt_deletion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiscussionComment", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DiscussionComment", "DeletedBy", c => c.Int(nullable: false));
            AddColumn("dbo.DiscussionComment", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiscussionComment", "DeletedOn");
            DropColumn("dbo.DiscussionComment", "DeletedBy");
            DropColumn("dbo.DiscussionComment", "IsDeleted");
        }
    }
}
