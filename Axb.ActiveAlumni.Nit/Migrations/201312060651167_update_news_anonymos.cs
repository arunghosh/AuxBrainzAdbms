namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_news_anonymos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlumniNews", "EmailAddress", c => c.String());
            AddColumn("dbo.AlumniNews", "UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.AlumniNews", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlumniNews", "Status");
            DropColumn("dbo.AlumniNews", "UserName");
            DropColumn("dbo.AlumniNews", "EmailAddress");
        }
    }
}
