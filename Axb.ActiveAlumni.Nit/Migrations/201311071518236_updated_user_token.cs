namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_token : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "EmailConfirmationToken", c => c.String(maxLength: 64));
            AlterColumn("dbo.User", "PasswordResetToken", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "PasswordResetToken", c => c.String(maxLength: 32));
            AlterColumn("dbo.User", "EmailConfirmationToken", c => c.String(maxLength: 32));
        }
    }
}
