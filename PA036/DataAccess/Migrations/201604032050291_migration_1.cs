namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "StockCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "StockCount");
        }
    }
}
