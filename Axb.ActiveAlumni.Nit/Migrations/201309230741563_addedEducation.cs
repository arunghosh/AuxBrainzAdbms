namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEducation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Education",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(maxLength: 128),
                        Location = c.String(maxLength: 128),
                        Degree = c.String(maxLength: 64),
                        Specialisation = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EducationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Education");
        }
    }
}
