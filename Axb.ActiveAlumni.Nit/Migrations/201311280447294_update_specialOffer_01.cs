namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_specialOffer_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "ImageType", c => c.String(maxLength: 256));
            AlterColumn("dbo.SpecialOffer", "ImageType", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpecialOffer", "ImageType", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "ImageType", c => c.String(maxLength: 32));
        }
    }
}
