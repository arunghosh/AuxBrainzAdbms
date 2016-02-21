namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_session_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Session", "UserName", c => c.String(maxLength: 128, defaultValue:"User_Name"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Session", "UserName");
        }
    }
}
