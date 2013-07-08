namespace CoolApp.Infraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFriendship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.FriendId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.FriendId)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Friendships", new[] { "FriendId" });
            DropIndex("dbo.Friendships", new[] { "UserId" });
            DropForeignKey("dbo.Friendships", "FriendId", "dbo.Users");
            DropForeignKey("dbo.Friendships", "UserId", "dbo.Users");
            DropTable("dbo.Friendships");
        }
    }
}
