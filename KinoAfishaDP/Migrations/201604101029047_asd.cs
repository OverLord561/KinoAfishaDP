namespace KinoAfishaDP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieHouses", "MovieHouseImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieHouses", "MovieHouseImage");
        }
    }
}
