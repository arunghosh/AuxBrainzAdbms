namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_digest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "DigestDaysSpan", c => c.Int(nullable: false));
            AddColumn("dbo.User", "TsMailDigest", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "TsMailDigest");
            DropColumn("dbo.User", "DigestDaysSpan");
        }
    }
}
