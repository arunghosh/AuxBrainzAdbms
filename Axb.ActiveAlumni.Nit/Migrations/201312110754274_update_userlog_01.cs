namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_userlog_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLog", "Comment", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserLog", "Comment", c => c.String(maxLength: 512));
        }
    }
}
