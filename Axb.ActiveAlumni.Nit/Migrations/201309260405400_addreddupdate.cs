namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreddupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "CurrentCity", c => c.String(maxLength: 128));
            AddColumn("dbo.User", "CurrentCountry", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "CurrentCountry");
            DropColumn("dbo.User", "CurrentCity");
        }
    }
}
