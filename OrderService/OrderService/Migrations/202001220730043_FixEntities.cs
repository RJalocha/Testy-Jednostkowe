namespace OrderService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rates", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Orders", "Product_Id", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Product_Id" });
            DropIndex("dbo.Rates", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Orders", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.Rates", name: "RatingUser_Id", newName: "RatingUserId");
            RenameIndex(table: "dbo.Rates", name: "IX_RatingUser_Id", newName: "IX_RatingUserId");
            CreateTable(
                "dbo.RateProducts",
                c => new
                    {
                        Rate_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rate_Id, t.Product_Id })
                .ForeignKey("dbo.Rates", t => t.Rate_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Rate_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Rates", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Rates", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rates", "Product_Id", c => c.Int());
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RateProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.RateProducts", "Rate_Id", "dbo.Rates");
            DropIndex("dbo.RateProducts", new[] { "Product_Id" });
            DropIndex("dbo.RateProducts", new[] { "Rate_Id" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            AlterColumn("dbo.Orders", "ProductId", c => c.Int());
            DropColumn("dbo.Rates", "ProductId");
            DropTable("dbo.RateProducts");
            RenameIndex(table: "dbo.Rates", name: "IX_RatingUserId", newName: "IX_RatingUser_Id");
            RenameColumn(table: "dbo.Rates", name: "RatingUserId", newName: "RatingUser_Id");
            RenameColumn(table: "dbo.Orders", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.Rates", "Product_Id");
            CreateIndex("dbo.Orders", "Product_Id");
            AddForeignKey("dbo.Orders", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.Rates", "Product_Id", "dbo.Products", "Id");
        }
    }
}
