namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_chapter_10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapter", "Latitude", c => c.String(maxLength: 32));
            AddColumn("dbo.Chapter", "Longitute", c => c.String(maxLength: 32));
            AddColumn("dbo.Chapter", "Email", c => c.String(maxLength: 64));
            AddColumn("dbo.Chapter", "Phone", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapter", "Phone");
            DropColumn("dbo.Chapter", "Email");
            DropColumn("dbo.Chapter", "Longitute");
            DropColumn("dbo.Chapter", "Latitude");
        }
    }
}
