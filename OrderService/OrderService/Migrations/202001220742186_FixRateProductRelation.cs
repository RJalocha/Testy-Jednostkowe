namespace OrderService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRateProductRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RateProducts", "Rate_Id", "dbo.Rates");
            DropForeignKey("dbo.RateProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.RateProducts", new[] { "Rate_Id" });
            DropIndex("dbo.RateProducts", new[] { "Product_Id" });
            CreateIndex("dbo.Rates", "ProductId");
            AddForeignKey("dbo.Rates", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropTable("dbo.RateProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RateProducts",
                c => new
                    {
                        Rate_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rate_Id, t.Product_Id });
            
            DropForeignKey("dbo.Rates", "ProductId", "dbo.Products");
            DropIndex("dbo.Rates", new[] { "ProductId" });
            CreateIndex("dbo.RateProducts", "Product_Id");
            CreateIndex("dbo.RateProducts", "Rate_Id");
            AddForeignKey("dbo.RateProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RateProducts", "Rate_Id", "dbo.Rates", "Id", cascadeDelete: true);
        }
    }
}
