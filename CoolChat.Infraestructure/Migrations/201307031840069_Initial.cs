namespace CoolChat.Infraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        UserFromId = c.Int(nullable: false),
                        UserToId = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 500),
                        IsBlocked = c.Boolean(nullable: false),
                        BlockingReason = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Users", t => t.UserFromId)
                .ForeignKey("dbo.Users", t => t.UserToId)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Chats", new[] { "UserToId" });
            DropIndex("dbo.Chats", new[] { "UserFromId" });
            DropForeignKey("dbo.Chats", "UserToId", "dbo.Users");
            DropForeignKey("dbo.Chats", "UserFromId", "dbo.Users");
            DropTable("dbo.Chats");
            DropTable("dbo.Users");
        }
    }
}
