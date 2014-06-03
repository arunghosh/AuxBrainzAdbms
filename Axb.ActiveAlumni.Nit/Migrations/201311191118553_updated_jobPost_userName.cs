namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_jobPost_userName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpening", "UserName", c => c.String(nullable: false, maxLength: 128, defaultValue:"User_Name"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobOpening", "UserName");
        }
    }
}
