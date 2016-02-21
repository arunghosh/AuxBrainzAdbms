namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_jobOpening_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpening", "Position", c => c.String(maxLength: 128));
            AddColumn("dbo.JobOpening", "DesiredSkills", c => c.String(maxLength: 512));
            DropColumn("dbo.JobOpening", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobOpening", "Tags", c => c.String(maxLength: 124));
            DropColumn("dbo.JobOpening", "DesiredSkills");
            DropColumn("dbo.JobOpening", "Position");
        }
    }
}
