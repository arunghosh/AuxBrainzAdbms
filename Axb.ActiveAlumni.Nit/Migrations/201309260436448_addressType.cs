namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "AddressType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "AddressType");
        }
    }
}
