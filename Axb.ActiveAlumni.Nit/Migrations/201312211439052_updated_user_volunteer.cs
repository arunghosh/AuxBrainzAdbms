namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_volunteer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "VolunteerInterest", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "VolunteerInterest");
        }
    }
}
