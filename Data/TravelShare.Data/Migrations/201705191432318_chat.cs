namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TripId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.TripId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ChatId = c.String(nullable: false),
                        Content = c.String(nullable: false, maxLength: 255),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Chat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.IsDeleted)
                .Index(t => t.Chat_Id);
            
            AddColumn("dbo.Trips", "ChatId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chats", "Id", "dbo.Trips");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropIndex("dbo.Messages", new[] { "IsDeleted" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Chats", new[] { "IsDeleted" });
            DropIndex("dbo.Chats", new[] { "TripId" });
            DropIndex("dbo.Chats", new[] { "Id" });
            DropColumn("dbo.Trips", "ChatId");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
        }
    }
}
