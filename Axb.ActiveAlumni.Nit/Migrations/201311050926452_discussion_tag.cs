namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discussion_tag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "Tags", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussion", "Tags");
        }
    }
}
