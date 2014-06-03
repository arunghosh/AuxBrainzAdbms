namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userrolecode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "RoleCode", c => c.Byte(nullable: false, defaultValue:1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "RoleCode");
        }
    }
}
