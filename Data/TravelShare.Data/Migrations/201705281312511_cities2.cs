namespace TravelShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cities2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.Trips", "FromCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Trips", "ToCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "FromCityId");
            CreateIndex("dbo.Trips", "ToCityId");
            AddForeignKey("dbo.Trips", "FromCityId", "dbo.Cities", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Trips", "ToCityId", "dbo.Cities", "Id", cascadeDelete: false);
            DropColumn("dbo.Trips", "From");
            DropColumn("dbo.Trips", "To");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "To", c => c.String(nullable: false));
            AddColumn("dbo.Trips", "From", c => c.String(nullable: false));
            DropForeignKey("dbo.Trips", "ToCityId", "dbo.Cities");
            DropForeignKey("dbo.Trips", "FromCityId", "dbo.Cities");
            DropIndex("dbo.Trips", new[] { "ToCityId" });
            DropIndex("dbo.Trips", new[] { "FromCityId" });
            DropIndex("dbo.Cities", new[] { "IsDeleted" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropColumn("dbo.Trips", "ToCityId");
            DropColumn("dbo.Trips", "FromCityId");
            DropTable("dbo.Cities");
        }
    }
}
