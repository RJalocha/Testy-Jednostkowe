using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Providers
{
    public interface IProductProvider
    {
        Product GetById(int id);
        List<Product> GetAll();
    }

    public class ProductProvider : IProductProvider
    {
        public List<Product> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products
                    .Include("Rates")
                    .ToList();
            }
        }

        public Product GetById(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products
                    .Include("Rates")
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }
    }
}