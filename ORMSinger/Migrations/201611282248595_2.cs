namespace ORMSinger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Accords", new[] { "Name" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Accords", "Name", unique: true);
        }
    }
}
