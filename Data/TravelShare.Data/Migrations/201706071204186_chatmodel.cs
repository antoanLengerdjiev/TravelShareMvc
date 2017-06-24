namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "TripId", "dbo.Trips");
            DropIndex("dbo.Messages", new[] { "TripId" });
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
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.Messages", "ChatId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "ChatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "ChatId");
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
            DropColumn("dbo.Messages", "TripId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "TripId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Chats", "Id", "dbo.Trips");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Chats", new[] { "IsDeleted" });
            DropIndex("dbo.Chats", new[] { "Id" });
            DropColumn("dbo.Trips", "ChatId");
            DropColumn("dbo.Messages", "ChatId");
            DropTable("dbo.Chats");
            CreateIndex("dbo.Messages", "TripId");
            AddForeignKey("dbo.Messages", "TripId", "dbo.Trips", "Id", cascadeDelete: true);
        }
    }
}
