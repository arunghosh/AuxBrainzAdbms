namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
