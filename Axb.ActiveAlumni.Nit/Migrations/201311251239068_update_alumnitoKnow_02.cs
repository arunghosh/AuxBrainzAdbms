namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_alumnitoKnow_02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AlumniSpeak", "Batch", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlumniSpeak", "Batch", c => c.Int(nullable: false));
        }
    }
}
