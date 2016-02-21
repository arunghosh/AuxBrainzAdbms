namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobPost_updateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobOpening", "UpdatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobOpening", "UpdatedOn");
        }
    }
}
