namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_msgUserMap_notify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageUserMap", "IsNotify", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageUserMap", "IsNotify");
        }
    }
}
