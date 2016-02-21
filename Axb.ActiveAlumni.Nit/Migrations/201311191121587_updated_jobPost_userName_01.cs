namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_jobPost_userName_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobOpening", "UserName", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOpening", "UserName", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
