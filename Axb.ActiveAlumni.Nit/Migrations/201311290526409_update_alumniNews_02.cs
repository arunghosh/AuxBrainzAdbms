namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_alumniNews_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlumniNews", "ImageType", c => c.String(maxLength: 256));
            AddColumn("dbo.AlumniNews", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlumniNews", "ImageData");
            DropColumn("dbo.AlumniNews", "ImageType");
        }
    }
}
