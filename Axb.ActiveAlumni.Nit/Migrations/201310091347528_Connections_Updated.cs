namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Connections_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connection", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Connection", "Status");
        }
    }
}
