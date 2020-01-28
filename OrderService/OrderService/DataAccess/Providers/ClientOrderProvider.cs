using OrderService.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.DataAccess.Providers
{
    public interface IClientOrderProvider
    {
        ClientOrder GetById( int id );
        List<ClientOrder> GetAll();
    }

    public class ClientOrderProvider : IClientOrderProvider
    {
        public List<ClientOrder> GetAll() {
            using( ApplicationDbContext db = new ApplicationDbContext() ) {
                return db.ClientOrders
                    .Include( "Orders" )
                    .Include( "Orders.Product" )
                    .Include( "Orders.Product.Rates" )
                    .Include( "Orders.Product.Rates.RatingUser" )
                    .Include( "Client" )
                    .Include( "Delivery" )
                    .ToList();
            }
        }

        public ClientOrder GetById( int id ) {
            using( ApplicationDbContext db = new ApplicationDbContext() ) {
                return db.ClientOrders
                    .Include( "Orders" )
                    .Include( "Orders.Product" )
                    .Include( "Orders.Product.Rates" )
                    .Include( "Orders.Product.Rates.RatingUser" )
                    .Include( "Client" )
                    .Include( "Delivery" )
                    .Where( p => p.Id == id )
                    .FirstOrDefault();
            }
        }
    }
}