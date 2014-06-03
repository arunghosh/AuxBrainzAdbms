namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_msgLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobOpening", "Title", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.JobOpening", "Description", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(maxLength: 1024));
            AlterColumn("dbo.MentorMessage", "Text", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.Message", "Text", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.Discussion", "Title", c => c.String(maxLength: 256));
            AlterColumn("dbo.DiscussionComment", "Text", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DiscussionComment", "Text", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Discussion", "Title", c => c.String(maxLength: 128));
            AlterColumn("dbo.Message", "Text", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.MentorMessage", "Text", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(maxLength: 512));
            AlterColumn("dbo.JobOpening", "Description", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.JobOpening", "Title", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
