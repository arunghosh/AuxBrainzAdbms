namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ImageType", c => c.String(maxLength: 32));
            AddColumn("dbo.User", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ImageData");
            DropColumn("dbo.User", "ImageType");
        }
    }
}
