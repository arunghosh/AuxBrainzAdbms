namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_msg_receiver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "ReceiverName", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "ReceiverName");
        }
    }
}
