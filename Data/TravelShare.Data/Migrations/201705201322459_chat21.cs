namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Chats", "Id", "dbo.Trips");
            DropIndex("dbo.Chats", new[] { "Id" });
            DropIndex("dbo.Chats", new[] { "TripId" });
            DropIndex("dbo.Chats", new[] { "IsDeleted" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Ratings", new[] { "IsDeleted" });
            DropIndex("dbo.Ratings", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Messages", "TripId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "TripId");
            AddForeignKey("dbo.Messages", "TripId", "dbo.Trips", "Id", cascadeDelete: false);
            DropColumn("dbo.Messages", "ChatId");
            DropColumn("dbo.Trips", "ChatId");
            DropTable("dbo.Chats");
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecieverId = c.String(nullable: false),
                        Points = c.Int(nullable: false),
                        GiverId = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trips", "ChatId", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "ChatId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Messages", "TripId", "dbo.Trips");
            DropIndex("dbo.Messages", new[] { "TripId" });
            DropColumn("dbo.Messages", "TripId");
            CreateIndex("dbo.Ratings", "ApplicationUser_Id");
            CreateIndex("dbo.Ratings", "IsDeleted");
            CreateIndex("dbo.Messages", "ChatId");
            CreateIndex("dbo.Chats", "IsDeleted");
            CreateIndex("dbo.Chats", "TripId");
            CreateIndex("dbo.Chats", "Id");
            AddForeignKey("dbo.Chats", "Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Ratings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
        }
    }
}
