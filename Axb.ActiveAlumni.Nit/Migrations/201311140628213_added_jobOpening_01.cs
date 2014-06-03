namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_jobOpening_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpening", "Title", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.JobOpening", "Location", c => c.String(maxLength: 128));
            AddColumn("dbo.JobOpening", "Organisation", c => c.String(maxLength: 128));
            AddColumn("dbo.JobOpening", "JobType", c => c.Int(nullable: false));
            AlterColumn("dbo.JobOpening", "Description", c => c.String(nullable: false, maxLength: 512));
            DropColumn("dbo.JobOpening", "JobTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobOpening", "JobTitle", c => c.String(maxLength: 128));
            AlterColumn("dbo.JobOpening", "Description", c => c.String(maxLength: 512));
            DropColumn("dbo.JobOpening", "JobType");
            DropColumn("dbo.JobOpening", "Organisation");
            DropColumn("dbo.JobOpening", "Location");
            DropColumn("dbo.JobOpening", "Title");
        }
    }
}
