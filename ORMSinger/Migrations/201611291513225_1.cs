namespace ORMSinger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        Views = c.Int(nullable: false),
                        Url = c.String(),
                        SingerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Singers", t => t.SingerId)
                .Index(t => t.SingerId);
            
            CreateTable(
                "dbo.Singers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Biography = c.String(),
                        Views = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SongAccords",
                c => new
                    {
                        Song_Id = c.Int(nullable: false),
                        Accord_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_Id, t.Accord_Id })
                .ForeignKey("dbo.Songs", t => t.Song_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accords", t => t.Accord_Id, cascadeDelete: true)
                .Index(t => t.Song_Id)
                .Index(t => t.Accord_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "SingerId", "dbo.Singers");
            DropForeignKey("dbo.SongAccords", "Accord_Id", "dbo.Accords");
            DropForeignKey("dbo.SongAccords", "Song_Id", "dbo.Songs");
            DropIndex("dbo.SongAccords", new[] { "Accord_Id" });
            DropIndex("dbo.SongAccords", new[] { "Song_Id" });
            DropIndex("dbo.Songs", new[] { "SingerId" });
            DropTable("dbo.SongAccords");
            DropTable("dbo.Singers");
            DropTable("dbo.Songs");
            DropTable("dbo.Accords");
        }
    }
}
