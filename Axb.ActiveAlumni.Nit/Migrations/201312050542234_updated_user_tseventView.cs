namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_tseventView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "TsEventView", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "TsEventView");
        }
    }
}
