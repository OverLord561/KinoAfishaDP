namespace KinoAfishaDP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class raveIt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "FilmSum", c => c.Double(nullable: false));
            AddColumn("dbo.Films", "FilmRave", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "FilmRave");
            DropColumn("dbo.Films", "FilmSum");
        }
    }
}
