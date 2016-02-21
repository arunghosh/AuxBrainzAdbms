namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_rolenotReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "RoleString", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "RoleString", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
