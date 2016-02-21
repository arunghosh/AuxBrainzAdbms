namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_namesMentor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MentorShip", "AlumniName", c => c.String(nullable: false, maxLength: 128, defaultValue:"Alumni"));
            AddColumn("dbo.MentorShip", "StudentName", c => c.String(nullable: false, maxLength: 128, defaultValue:"Student"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MentorShip", "StudentName");
            DropColumn("dbo.MentorShip", "AlumniName");
        }
    }
}
