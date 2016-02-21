namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eduReqAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Education", "SchoolName", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Education", "SchoolName", c => c.String(maxLength: 128));
        }
    }
}
