namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_joOpen_expNUll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobOpening", "ExperienceFrom", c => c.Int());
            AlterColumn("dbo.JobOpening", "ExperienceTo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobOpening", "ExperienceTo", c => c.Int(nullable: false));
            AlterColumn("dbo.JobOpening", "ExperienceFrom", c => c.Int(nullable: false));
        }
    }
}
