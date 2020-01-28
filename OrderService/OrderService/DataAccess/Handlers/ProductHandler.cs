using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Handlers
{
    public interface IProductHandler
    {
        int Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }

    public class ProductHandler : IProductHandler
    {
        public int Add(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product p = db.Products.Add(product);
                db.SaveChanges();
                return p.Id;
            }
        }

        public bool Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product p = db.Products.Where(x => x.Id == id).FirstOrDefault();
                if (p != null)
                {
                    db.Products.Remove(p);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Update(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product p = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
                if (p != null)
                {
                    p.Name = product.Name;
                    p.Description = product.Description;
                    p.Price = product.Price;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}