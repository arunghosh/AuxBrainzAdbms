namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_news_anonymos_10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AlumniNews", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlumniNews", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
