namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewPropertiesToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Trips", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "Date");
            DropColumn("dbo.Trips", "Description");
        }
    }
}
