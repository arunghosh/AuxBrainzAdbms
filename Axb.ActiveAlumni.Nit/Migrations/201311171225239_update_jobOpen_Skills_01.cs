namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_jobOpen_Skills_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
