namespace OrderService.Migrations
{
    using OrderService.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OrderService.Models.ApplicationDbContext>
    {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed( OrderService.Models.ApplicationDbContext context ) {

            var products = new List<Product>
            {
                new Product()
                {
                    Id = 1, Name = "Product1", Description = "Product1 Description", Price = 1.25m, Rates = new List<Rate>()
                    {
                        new Rate() { Id = 1, Description="Rate1", Points = 1 },
                        new Rate() { Id = 2, Description="Rate2", Points = 2 }
                    }
                },
                new Product()
                {
                    Id = 2, Name = "Product2", Description = "Product2 Description", Price = 2.45m, Rates = new List<Rate>()
                    {
                        new Rate() { Id = 3, Description="Rate3", Points = 3 },
                        new Rate() { Id = 4, Description="Rate4", Points = 3 }
                    }
                }
            };
            products.ForEach( s => context.Products.AddOrUpdate( p => p.Id, s ) );
            context.SaveChanges();


            var deliveries = new List<Delivery>
            {
                new Delivery()
                {
                    Id = 1, Name = "Delivery1", Price = 0.50m, DeliveryDays = 1,
                },
                new Delivery()
                {
                    Id = 2, Name = "Delivery2", Price = 0.25m, DeliveryDays = 2
                }
            };
            deliveries.ForEach( s => context.Deliveries.AddOrUpdate( d => d.Id, s ) );
            context.SaveChanges();

            // var users = new List<ApplicationUser>() {
            //     new ApplicationUser() {
            //         Id = Guid.NewGuid().ToString(), // new Guid().ToString(),
            //         Email = "user1@orderservice.pl",
            //         PasswordHash = "ABjRAYHyUI4M6Tet/Q8GDqCZ8TxZhqz7+9CMJn0qzVGTFDK2k4w0b8wjX5HLcP/PpA==",
            //         SecurityStamp = "3e507711-73a8-4441-a314-4bb04108641b",
            //         LockoutEnabled = true,
            //         UserName = "user1@orderservice.pl"
            //     },
            //     new ApplicationUser() {
            //         Id = Guid.NewGuid().ToString(), // new Guid().ToString(),
            //         Email = "user2@orderservice.pl",
            //         PasswordHash = "ABjRAYHyUI4M6Tet/Q8GDqCZ8TxZhqz7+9CMJn0qzVGTFDK2k4w0b8wjX5HLcP/PpA==",
            //         SecurityStamp = "3e507711-73a8-4441-a314-4bb04108641b",
            //         LockoutEnabled = true,
            //         UserName = "user2@orderservice.pl"
            //     },
            //     new ApplicationUser() {
            //         Id = Guid.NewGuid().ToString(), // new Guid().ToString(),
            //         Email = "user3@orderservice.pl",
            //         PasswordHash = "ABjRAYHyUI4M6Tet/Q8GDqCZ8TxZhqz7+9CMJn0qzVGTFDK2k4w0b8wjX5HLcP/PpA==",
            //         SecurityStamp = "3e507711-73a8-4441-a314-4bb04108641b",
            //         LockoutEnabled = true,
            //         UserName = "user3@orderservice.pl"
            //     },
            //     new ApplicationUser() {
            //         Id = Guid.NewGuid().ToString(), // new Guid().ToString(),
            //         Email = "admin@orderservice.pl",
            //         PasswordHash = "ABjRAYHyUI4M6Tet/Q8GDqCZ8TxZhqz7+9CMJn0qzVGTFDK2k4w0b8wjX5HLcP/PpA==",
            //         SecurityStamp = "3e507711-73a8-4441-a314-4bb04108641b",
            //         LockoutEnabled = true,
            //         UserName = "admin@orderservice.pl"
            //     }
            // };
            // users.ForEach( s => context.Users.AddOrUpdate( u => u.Id, s ) );
            // context.SaveChanges();

            // var orders = new List<ClientOrder>
            // {
            //     new ClientOrder()
            //     {
            //         Id = 1,
            //         Orders = new List<Order>
            //         {
            //             new Order { Id = 1, Product = products.Find(p => p.Id == 1), Quantity = 3, TotalPrice = 3.75m },
            //             new Order { Id = 2, Product = products.Find(p => p.Id == 2), Quantity = 2, TotalPrice = 4.90m },
            //         },
            //         Discount = 0.65m,
            //         TotalPrice = 8.50m,
            //         Client = context.Users.Where(u => u.UserName == "user1@orderservice.pl").FirstOrDefault(),
            //         Delivery = deliveries.Find(d => d.Id == 1),
            //         CreationDate = DateTime.Now,
            //         DeliveryDate = DateTime.Now.AddDays(2),
            //     },
            //     new ClientOrder()
            //     {
            //         Id = 2,
            //         Orders = new List<Order>
            //         {
            //             new Order { Id = 3, Product = products.Find(p => p.Id == 1), Quantity = 10, TotalPrice = 12.50m },
            //             new Order { Id = 4, Product = products.Find(p => p.Id == 2), Quantity = 20, TotalPrice = 49.00m },
            //         },
            //         Discount = 1.50m,
            //         TotalPrice = 60.25m,
            //         Client = context.Users.Where(u => u.UserName == "user2@orderservice.pl").FirstOrDefault(),
            //         Delivery = deliveries.Find(d => d.Id == 2),
            //         CreationDate = DateTime.Now.AddDays(10),
            //         DeliveryDate = DateTime.Now.AddDays(12),
            //     },
            // };
            // orders.ForEach( s => context.ClientOrders.AddOrUpdate( o => o.Id, s ) );
            // context.SaveChanges();



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
