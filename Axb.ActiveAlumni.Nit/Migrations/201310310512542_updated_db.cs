namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_db : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DiscussionComment", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DiscussionComment", "Date", c => c.String());
        }
    }
}
