namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectingTripToPassengers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Trips", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Trip_Id" });
            CreateTable(
                "dbo.ApplicationUserTrips",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Trip_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Trip_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: false)
                .ForeignKey("dbo.Trips", t => t.Trip_Id, cascadeDelete: false)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Trip_Id);
            
            DropColumn("dbo.Trips", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Trip_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Trip_Id", c => c.Int());
            AddColumn("dbo.Trips", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserTrips", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.ApplicationUserTrips", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserTrips", new[] { "Trip_Id" });
            DropIndex("dbo.ApplicationUserTrips", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserTrips");
            CreateIndex("dbo.AspNetUsers", "Trip_Id");
            CreateIndex("dbo.Trips", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Trips", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
