namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPictureEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccomodationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationID, cascadeDelete: true)
                .Index(t => t.AccomodationID)
                .Index(t => t.PictureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomodationPictures", "AccomodationID", "dbo.Accomodations");
            DropForeignKey("dbo.AccomodationPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccomodationPackagePictures", "AccomodationPackageID", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackagePictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.AccomodationPictures", new[] { "PictureID" });
            DropIndex("dbo.AccomodationPictures", new[] { "AccomodationID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "PictureID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "AccomodationPackageID" });
            DropTable("dbo.AccomodationPictures");
            DropTable("dbo.Pictures");
            DropTable("dbo.AccomodationPackagePictures");
        }
    }
}
