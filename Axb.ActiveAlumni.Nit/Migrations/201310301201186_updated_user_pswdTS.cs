namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_pswdTS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PasswordChangedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PasswordChangedOn");
        }
    }
}
