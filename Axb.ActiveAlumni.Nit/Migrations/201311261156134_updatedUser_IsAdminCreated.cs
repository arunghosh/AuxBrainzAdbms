namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedUser_IsAdminCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsAdminCreated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsAdminCreated");
        }
    }
}
