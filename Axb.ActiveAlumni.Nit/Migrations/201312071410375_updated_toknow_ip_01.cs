namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_toknow_ip_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NonAdminNews", "Course", c => c.String(nullable: false, maxLength: 124));
            AlterColumn("dbo.NonAdminNews", "Batch", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.NonAdminNews", "AlumniName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.NonAdminNews", "News", c => c.String(nullable: false, maxLength: 2048));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NonAdminNews", "News", c => c.String(maxLength: 2048));
            AlterColumn("dbo.NonAdminNews", "AlumniName", c => c.String(maxLength: 128));
            AlterColumn("dbo.NonAdminNews", "Batch", c => c.String(maxLength: 10));
            AlterColumn("dbo.NonAdminNews", "Course", c => c.String(maxLength: 124));
        }
    }
}
