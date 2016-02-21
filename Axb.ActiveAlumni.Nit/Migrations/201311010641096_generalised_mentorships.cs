namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class generalised_mentorships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MentorMessage", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.MentorMessage", "SenderName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MentorMessage", "Text", c => c.String(nullable: false, maxLength: 512));
            DropColumn("dbo.MentorMessage", "UserId");
            DropColumn("dbo.MentorMessage", "UserName");
            DropColumn("dbo.MentorMessage", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MentorMessage", "Message", c => c.String(maxLength: 512));
            AddColumn("dbo.MentorMessage", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MentorMessage", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.MentorMessage", "Text");
            DropColumn("dbo.MentorMessage", "SenderName");
            DropColumn("dbo.MentorMessage", "SenderId");
        }
    }
}
