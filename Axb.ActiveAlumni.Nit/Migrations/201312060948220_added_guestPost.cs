namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_guestPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestPost",
                c => new
                    {
                        GuestPostId = c.Int(nullable: false, identity: true),
                        Course = c.String(maxLength: 124),
                        Batch = c.String(maxLength: 10),
                        AlumniName = c.String(maxLength: 128),
                        News = c.String(maxLength: 2048),
                        UserId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GuestPostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GuestPost");
        }
    }
}
