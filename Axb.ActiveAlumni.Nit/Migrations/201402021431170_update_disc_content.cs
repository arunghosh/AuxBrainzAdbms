namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_disc_content : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussion", "Content");
        }
    }
}
