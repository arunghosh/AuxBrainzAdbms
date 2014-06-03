namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        IPAddress = c.String(maxLength: 64),
                        UserName = c.String(maxLength: 128),
                        UserId = c.Int(nullable: false),
                        Email = c.String(),
                        Message = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedback");
        }
    }
}
