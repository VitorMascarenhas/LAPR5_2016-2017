namespace PortoGO.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GpsCoordinate",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Road",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 300),
                        Weight = c.Double(nullable: false),
                        Width = c.Single(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Route",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Hour = c.Time(nullable: false, precision: 7),
                        RunTime = c.Time(nullable: false, precision: 7),
                        PointOfInterestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visit", t => t.VisitId, cascadeDelete: true)
                .ForeignKey("dbo.PointOfInterest", t => t.PointOfInterestId, cascadeDelete: true)
                .Index(t => t.VisitId)
                .Index(t => t.PointOfInterestId);
            
            CreateTable(
                "dbo.PointOfInterest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 300),
                        BusinessHours_FromHour = c.Time(nullable: false, precision: 7),
                        BusinessHours_ToHour = c.Time(nullable: false, precision: 7),
                        LocationId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TimeTovisit = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LocationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Hashtag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(maxLength: 100),
                        UserId = c.String(maxLength: 128),
                        PointOfInterestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointOfInterest", t => t.PointOfInterestId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PointOfInterestId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Coordinates_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GpsCoordinate", t => t.Coordinates_Id)
                .Index(t => t.Coordinates_Id);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        Enddate = c.DateTime(nullable: false),
                        ReturnToStart = c.Boolean(nullable: false),
                        Duration = c.Int(nullable: false),
                        StartLocation_Id = c.Long(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.StartLocation_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.StartLocation_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserAuditTrail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        UserName = c.String(maxLength: 256),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoadGpsCoordinate",
                c => new
                    {
                        Road_Id = c.Int(nullable: false),
                        GpsCoordinate_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Road_Id, t.GpsCoordinate_Id })
                .ForeignKey("dbo.Road", t => t.Road_Id, cascadeDelete: true)
                .ForeignKey("dbo.GpsCoordinate", t => t.GpsCoordinate_Id, cascadeDelete: true)
                .Index(t => t.Road_Id)
                .Index(t => t.GpsCoordinate_Id);
            
            CreateTable(
                "dbo.RouteGpsCoordinate",
                c => new
                    {
                        Route_Id = c.Int(nullable: false),
                        GpsCoordinate_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Route_Id, t.GpsCoordinate_Id })
                .ForeignKey("dbo.Route", t => t.Route_Id, cascadeDelete: true)
                .ForeignKey("dbo.GpsCoordinate", t => t.GpsCoordinate_Id, cascadeDelete: true)
                .Index(t => t.Route_Id)
                .Index(t => t.GpsCoordinate_Id);
            
            CreateTable(
                "dbo.VisitPointOfInterest",
                c => new
                    {
                        Visit_Id = c.Int(nullable: false),
                        PointOfInterest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visit_Id, t.PointOfInterest_Id })
                .ForeignKey("dbo.Visit", t => t.Visit_Id, cascadeDelete: true)
                .ForeignKey("dbo.PointOfInterest", t => t.PointOfInterest_Id, cascadeDelete: true)
                .Index(t => t.Visit_Id)
                .Index(t => t.PointOfInterest_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Route", "PointOfInterestId", "dbo.PointOfInterest");
            DropForeignKey("dbo.Visit", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Visit", "StartLocation_Id", "dbo.Location");
            DropForeignKey("dbo.Route", "VisitId", "dbo.Visit");
            DropForeignKey("dbo.VisitPointOfInterest", "PointOfInterest_Id", "dbo.PointOfInterest");
            DropForeignKey("dbo.VisitPointOfInterest", "Visit_Id", "dbo.Visit");
            DropForeignKey("dbo.PointOfInterest", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PointOfInterest", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Location", "Coordinates_Id", "dbo.GpsCoordinate");
            DropForeignKey("dbo.Hashtag", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Hashtag", "PointOfInterestId", "dbo.PointOfInterest");
            DropForeignKey("dbo.RouteGpsCoordinate", "GpsCoordinate_Id", "dbo.GpsCoordinate");
            DropForeignKey("dbo.RouteGpsCoordinate", "Route_Id", "dbo.Route");
            DropForeignKey("dbo.RoadGpsCoordinate", "GpsCoordinate_Id", "dbo.GpsCoordinate");
            DropForeignKey("dbo.RoadGpsCoordinate", "Road_Id", "dbo.Road");
            DropIndex("dbo.VisitPointOfInterest", new[] { "PointOfInterest_Id" });
            DropIndex("dbo.VisitPointOfInterest", new[] { "Visit_Id" });
            DropIndex("dbo.RouteGpsCoordinate", new[] { "GpsCoordinate_Id" });
            DropIndex("dbo.RouteGpsCoordinate", new[] { "Route_Id" });
            DropIndex("dbo.RoadGpsCoordinate", new[] { "GpsCoordinate_Id" });
            DropIndex("dbo.RoadGpsCoordinate", new[] { "Road_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Visit", new[] { "User_Id" });
            DropIndex("dbo.Visit", new[] { "StartLocation_Id" });
            DropIndex("dbo.Location", new[] { "Coordinates_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Hashtag", new[] { "PointOfInterestId" });
            DropIndex("dbo.Hashtag", new[] { "UserId" });
            DropIndex("dbo.PointOfInterest", new[] { "UserId" });
            DropIndex("dbo.PointOfInterest", new[] { "LocationId" });
            DropIndex("dbo.Route", new[] { "PointOfInterestId" });
            DropIndex("dbo.Route", new[] { "VisitId" });
            DropTable("dbo.VisitPointOfInterest");
            DropTable("dbo.RouteGpsCoordinate");
            DropTable("dbo.RoadGpsCoordinate");
            DropTable("dbo.UserAuditTrail");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Visit");
            DropTable("dbo.Location");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Hashtag");
            DropTable("dbo.PointOfInterest");
            DropTable("dbo.Route");
            DropTable("dbo.Road");
            DropTable("dbo.GpsCoordinate");
        }
    }
}
