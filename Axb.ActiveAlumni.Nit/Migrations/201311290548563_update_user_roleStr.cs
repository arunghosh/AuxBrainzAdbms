namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user_roleStr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "RoleString", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "RoleString");
        }
    }
}
