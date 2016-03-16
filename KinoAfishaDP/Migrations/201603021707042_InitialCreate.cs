namespace KinoAfishaDP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmId = c.Int(nullable: false, identity: true),
                        FilmName = c.String(nullable: false),
                        FilmRating = c.String(nullable: false),
                        FilmCountry = c.String(nullable: false),
                        FilmAge = c.String(nullable: false),
                        FilmLength = c.Double(nullable: false),
                        FilmGenre = c.String(nullable: false),
                        FilmActors = c.String(nullable: false),
                        FilmReview = c.String(maxLength: 1000),
                        FilmPictures = c.String(nullable: false),
                        FilmPlus = c.Int(nullable: false),
                        FilmMinus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilmId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        SessionTimePokaz = c.DateTime(nullable: false),
                        FilmId = c.Int(nullable: false),
                        MovieHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .ForeignKey("dbo.MovieHouses", t => t.MovieHouseId, cascadeDelete: true)
                .Index(t => t.FilmId)
                .Index(t => t.MovieHouseId);
            
            CreateTable(
                "dbo.MovieHouses",
                c => new
                    {
                        MovieHouseId = c.Int(nullable: false, identity: true),
                        MovieHouseName = c.String(nullable: false),
                        MovieHouseTelephone = c.String(nullable: false),
                        MovieHouseAdress = c.String(nullable: false),
                        MovieHouseRating = c.Double(nullable: false),
                        MovieHouseStars = c.String(nullable: false),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MovieHouseId);
            
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewID = c.Int(nullable: false),
                        UserNickName = c.String(),
                        LabelText = c.String(nullable: false),
                        E_mail = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPhotoes",
                c => new
                    {
                        UserPhotoId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.UserPhotoId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sessions", new[] { "MovieHouseId" });
            DropIndex("dbo.Sessions", new[] { "FilmId" });
            DropForeignKey("dbo.Sessions", "MovieHouseId", "dbo.MovieHouses");
            DropForeignKey("dbo.Sessions", "FilmId", "dbo.Films");
            DropTable("dbo.UserPhotoes");
            DropTable("dbo.UserComments");
            DropTable("dbo.MovieHouses");
            DropTable("dbo.Sessions");
            DropTable("dbo.Films");
        }
    }
}
