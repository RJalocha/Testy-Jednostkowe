using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Providers
{
    public interface IDeliveryProvider
    {
        Delivery GetById(int id);
        List<Delivery> GetAll();
    }

    public class DeliveryProvider : IDeliveryProvider
    {
        public List<Delivery> GetAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Deliveries.ToList();
            }
        }

        public Delivery GetById(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Deliveries
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
            }
        }
    }
}