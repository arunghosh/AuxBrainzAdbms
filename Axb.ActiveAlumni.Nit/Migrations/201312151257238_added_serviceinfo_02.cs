namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_serviceinfo_02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceInfo", "Title", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ServiceInfo", "Category", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.ServiceInfo", "Location", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceInfo", "Location", c => c.String(maxLength: 128));
            AlterColumn("dbo.ServiceInfo", "Category", c => c.String(maxLength: 64));
            AlterColumn("dbo.ServiceInfo", "Title", c => c.String(maxLength: 128));
        }
    }
}
