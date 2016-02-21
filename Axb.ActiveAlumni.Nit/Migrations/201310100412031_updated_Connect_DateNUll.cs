namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Connect_DateNUll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Connection", "RespondedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Connection", "RespondedOn", c => c.DateTime(nullable: false));
        }
    }
}
