namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udpate_feedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedback", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Feedback", "Message", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedback", "Message", c => c.String(maxLength: 1024));
            DropColumn("dbo.Feedback", "Date");
        }
    }
}
