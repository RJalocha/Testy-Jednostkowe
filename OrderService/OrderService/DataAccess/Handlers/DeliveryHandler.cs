using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Handlers
{
    public interface IDeliveryHandler
    {
        int Add(Delivery delivery);
        bool Update(Delivery delivery);
        bool Delete(int id);
    }

    public class DeliveryHandler : IDeliveryHandler
    {
        public int Add(Delivery delivery)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Delivery d = db.Deliveries.Add(delivery);
                db.SaveChanges();
                return d.Id;
            }
        }

        public bool Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Delivery d = db.Deliveries.Where(x => x.Id == id).FirstOrDefault();
                if (d != null)
                {
                    db.Deliveries.Remove(d);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Update(Delivery delivery)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Delivery d = db.Deliveries.Where(x => x.Id == delivery.Id).FirstOrDefault();
                if (d != null)
                {
                    d.Name = delivery.Name;
                    d.Price = delivery.Price;
                    d.DeliveryDays = delivery.DeliveryDays;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}