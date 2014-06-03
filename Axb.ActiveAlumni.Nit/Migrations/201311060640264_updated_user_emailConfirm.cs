namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_emailConfirm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "EmailConfirmedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "EmailConfirmedOn");
        }
    }
}
