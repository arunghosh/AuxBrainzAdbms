namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_pswd_tkn_exp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PasswordExpiryTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PasswordExpiryTime");
        }
    }
}
