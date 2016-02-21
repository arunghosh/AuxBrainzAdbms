namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_creatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "CreateType", c => c.Int(nullable: false));
            DropColumn("dbo.User", "IsAdminCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "IsAdminCreated", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "CreateType");
        }
    }
}
