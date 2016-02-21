namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_alumnitoKnow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlumniToKnow", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlumniToKnow", "IsDeleted");
        }
    }
}
