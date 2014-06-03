namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udpate_specialOffers_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecialOffer", "Category", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpecialOffer", "Category");
        }
    }
}
