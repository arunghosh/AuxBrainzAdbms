namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_removed_pswd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Password", c => c.String(maxLength: 64));
        }
    }
}
