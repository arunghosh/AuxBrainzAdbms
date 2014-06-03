namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_offer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SpecialOffer", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpecialOffer", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
