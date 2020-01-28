using OrderService.Helpers;
using OrderService.Models;
using System;
using System.Linq;

namespace OrderService.DataAccess.Handlers
{
    public interface IClientOrderHandler
    {
        int Add( ClientOrder clientOrder );
        bool Update( ClientOrder clientOrder );
        bool Delete( int id );
    }

    public class ClientOrderHandler : IClientOrderHandler
    {
        public int Add( ClientOrder clientOrder ) {
            using( ApplicationDbContext db = new ApplicationDbContext() ) {
                clientOrder.CreationDate = DateTime.Now;
                clientOrder.DeliveryDate = DateTime.Now;
                ClientOrder co = db.ClientOrders.Add( clientOrder );
                db.SaveChanges();

                var clientOrderFromDB = db.ClientOrders
                    .Include("Orders")
                    .Include("Orders.Product")
                    .Include("Delivery")
                    .Where(x => x.Id == clientOrder.Id)
                    .FirstOrDefault();

                OrderTotalPriceCalculator orderTotalPriceCalculator = new OrderTotalPriceCalculator();
                foreach (Order o in clientOrderFromDB.Orders)
                {
                    o.TotalPrice = orderTotalPriceCalculator.Calculate(o);
                }
                db.SaveChanges();

                ClientOrderTotalPriceCalculator clientOrderTotalPriceCalculator = new ClientOrderTotalPriceCalculator();
                clientOrderFromDB.TotalPrice = clientOrderTotalPriceCalculator.Calculate(clientOrderFromDB);

                ClientOrderDeliveryDateCalculator clientOrderDeliveryDateCalculator = new ClientOrderDeliveryDateCalculator();
                clientOrderFromDB.DeliveryDate = clientOrderDeliveryDateCalculator.Calculate(clientOrderFromDB);
                db.SaveChanges();

                return co.Id;
            }
        }

        public bool Delete( int id ) {
            using( ApplicationDbContext db = new ApplicationDbContext() ) {
                ClientOrder co = db.ClientOrders.Where( x => x.Id == id ).FirstOrDefault();
                if( co != null ) {
                    db.ClientOrders.Remove( co );
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Update( ClientOrder clientOrder ) {
            using( ApplicationDbContext db = new ApplicationDbContext() ) {
                ClientOrder co = db.ClientOrders
                    .Include("Orders")
                    .Include("Orders.Product")
                    .Include("Delivery")
                    .Where( x => x.Id == clientOrder.Id )
                    .FirstOrDefault();
                if( co != null ) {
                    co.ClientId = clientOrder.ClientId;
                    co.Discount = clientOrder.Discount;
                    co.TotalPrice = clientOrder.TotalPrice;
                    co.DeliveryId = clientOrder.DeliveryId;

                    // Czyszczenie co.Orders
                    int ordersCount = co.Orders.Count;
                    for (int i = 0; i < ordersCount; i++)
                    {
                        var o = co.Orders.FirstOrDefault();
                        co.Orders.Remove(o);
                        db.Orders.Remove(o);
                    }
                    db.SaveChanges();
                    // Tworzenie nowych Orders
                    OrderTotalPriceCalculator orderTotalPriceCalculator = new OrderTotalPriceCalculator();
                    foreach ( Order order in clientOrder.Orders ) {
                        Order o = new Order();
                        o.ProductId = order.ProductId;
                        o.Quantity = order.Quantity;

                        var product = db.Products.Where(p => p.Id == order.ProductId).FirstOrDefault();
                        order.Product = product;
                        o.TotalPrice = orderTotalPriceCalculator.Calculate(order);

                        co.Orders.Add( o );
                    }
                    ClientOrderDeliveryDateCalculator clientOrderDeliveryDateCalculator = new ClientOrderDeliveryDateCalculator();
                    co.DeliveryDate = clientOrderDeliveryDateCalculator.Calculate(co);
                    db.SaveChanges();

                    var clientOrderFromDB = db.ClientOrders
                        .Include("Orders")
                        .Include("Orders.Product")
                        .Include("Delivery")
                        .Where(x => x.Id == clientOrder.Id)
                        .FirstOrDefault();
                    ClientOrderTotalPriceCalculator clientOrderTotalPriceCalculator = new ClientOrderTotalPriceCalculator();
                    clientOrderFromDB.TotalPrice = clientOrderTotalPriceCalculator.Calculate(clientOrderFromDB);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}