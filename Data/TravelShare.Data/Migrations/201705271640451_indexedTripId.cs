namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indexedTripId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "TripId" });
            CreateIndex("dbo.Messages", "TripId");
        }

        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "TripId" });
            CreateIndex("dbo.Messages", "TripId");
        }
    }
}
