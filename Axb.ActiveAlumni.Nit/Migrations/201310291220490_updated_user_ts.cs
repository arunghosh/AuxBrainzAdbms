namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_ts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "JoinedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "JoinedOn");
        }
    }
}
