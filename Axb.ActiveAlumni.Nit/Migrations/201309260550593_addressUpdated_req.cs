namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressUpdated_req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Address", "City", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Address", "Country", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Address", "Country", c => c.String(maxLength: 64));
            AlterColumn("dbo.Address", "City", c => c.String(maxLength: 64));
        }
    }
}
