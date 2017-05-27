namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropColumn("dbo.Messages", "ChatId");
            RenameColumn(table: "dbo.Messages", name: "Chat_Id", newName: "ChatId");
            AlterColumn("dbo.Messages", "ChatId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "ChatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "ChatId");
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ChatId" });
            AlterColumn("dbo.Messages", "ChatId", c => c.Int());
            AlterColumn("dbo.Messages", "ChatId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Messages", name: "ChatId", newName: "Chat_Id");
            AddColumn("dbo.Messages", "ChatId", c => c.String(nullable: false));
            CreateIndex("dbo.Messages", "Chat_Id");
            AddForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
