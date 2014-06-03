namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Circles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Circle", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Circle", "Date");
        }
    }
}
