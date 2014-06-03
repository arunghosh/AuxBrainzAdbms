namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_dashboard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FailedLogin", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.NonAdminNews", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Feedback", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Log", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Log", "IsRead");
            DropColumn("dbo.Feedback", "IsRead");
            DropColumn("dbo.NonAdminNews", "IsRead");
            DropColumn("dbo.FailedLogin", "IsRead");
        }
    }
}
