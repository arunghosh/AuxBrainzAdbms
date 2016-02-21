namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_notify_user_01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MessageUserMap", "IsRead");
            DropColumn("dbo.MessageUserMap", "IsNotify");
            DropColumn("dbo.MessageUserMap", "LastSendOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageUserMap", "LastSendOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageUserMap", "IsNotify", c => c.Boolean(nullable: false));
            AddColumn("dbo.MessageUserMap", "IsRead", c => c.Boolean(nullable: false));
        }
    }
}
