namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobPost_update_desiredSkill : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(nullable: false, maxLength: 512, defaultValue:"Good Communication"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOpening", "DesiredSkills", c => c.String(maxLength: 512));
        }
    }
}
