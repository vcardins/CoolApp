namespace CoolChat.Infraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordChanged", c => c.DateTime());
            AddColumn("dbo.Users", "IsAccountVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsLoginAllowed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsAccountClosed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "AccountClosed", c => c.DateTime());
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime());
            AddColumn("dbo.Users", "LastFailedLogin", c => c.DateTime());
            AddColumn("dbo.Users", "FailedLoginCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "VerificationKey", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "VerificationKeySent", c => c.DateTime());
            AddColumn("dbo.Users", "HashedPassword", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false));
            DropColumn("dbo.Users", "HashedPassword");
            DropColumn("dbo.Users", "VerificationKeySent");
            DropColumn("dbo.Users", "VerificationKey");
            DropColumn("dbo.Users", "FailedLoginCount");
            DropColumn("dbo.Users", "LastFailedLogin");
            DropColumn("dbo.Users", "LastLogin");
            DropColumn("dbo.Users", "AccountClosed");
            DropColumn("dbo.Users", "IsAccountClosed");
            DropColumn("dbo.Users", "IsLoginAllowed");
            DropColumn("dbo.Users", "IsAccountVerified");
            DropColumn("dbo.Users", "PasswordChanged");
        }
    }
}
