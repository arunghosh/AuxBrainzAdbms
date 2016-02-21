namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chapter_added_fburl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapter", "FacebookUrl", c => c.String(maxLength: 128));
            AddColumn("dbo.Chapter", "WebsiteUrl", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapter", "WebsiteUrl");
            DropColumn("dbo.Chapter", "FacebookUrl");
        }
    }
}
