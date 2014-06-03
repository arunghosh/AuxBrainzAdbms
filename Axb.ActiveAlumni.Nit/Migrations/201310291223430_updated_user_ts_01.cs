namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_ts_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ApprovedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ApprovedOn");
        }
    }
}
