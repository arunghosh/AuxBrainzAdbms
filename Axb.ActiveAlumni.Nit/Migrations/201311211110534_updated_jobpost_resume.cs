namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_jobpost_resume : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpening", "SendToAdmin", c => c.Boolean(nullable: false, defaultValue:false));
            AlterColumn("dbo.JobOpening", "SendYourResumesTo", c => c.String(nullable: false, defaultValue: "admin@nitcaa.com"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOpening", "SendYourResumesTo", c => c.String());
            DropColumn("dbo.JobOpening", "SendToAdmin");
        }
    }
}
