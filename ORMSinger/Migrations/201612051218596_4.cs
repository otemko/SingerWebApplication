namespace ORMSinger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "SingerId", "dbo.Singers");
            DropIndex("dbo.Songs", new[] { "SingerId" });
            AlterColumn("dbo.Songs", "SingerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "SingerId");
            AddForeignKey("dbo.Songs", "SingerId", "dbo.Singers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "SingerId", "dbo.Singers");
            DropIndex("dbo.Songs", new[] { "SingerId" });
            AlterColumn("dbo.Songs", "SingerId", c => c.Int());
            CreateIndex("dbo.Songs", "SingerId");
            AddForeignKey("dbo.Songs", "SingerId", "dbo.Singers", "Id");
        }
    }
}
