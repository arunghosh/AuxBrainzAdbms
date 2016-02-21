namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_alumniNews_03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AlumniNews", "News", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlumniNews", "News", c => c.String(maxLength: 2024));
        }
    }
}
