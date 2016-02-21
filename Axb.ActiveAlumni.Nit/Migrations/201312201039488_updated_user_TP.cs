namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_TP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsTouchPoint", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsTouchPoint");
        }
    }
}
