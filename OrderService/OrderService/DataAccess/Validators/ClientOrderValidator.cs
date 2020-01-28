using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.DataAccess.Validators
{
    public interface IClientOrderValidator
    {
        List<KeyValuePair<string, string>> CanAddClientOrder( ClientOrder clientOrder );
        List<KeyValuePair<string, string>> CanUpdateClientOrder( ClientOrder clientOrder );
    }

    public class ClientOrderValidator : IClientOrderValidator
    {
        public List<KeyValuePair<string, string>> CanAddClientOrder( ClientOrder clientOrder ) {
            if( clientOrder == null ) {
                throw new ArgumentNullException( "ClientOrder can not be null" );
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(clientOrder.ClientId))
            {
                errors.Add(new KeyValuePair<string, string>("ClientId", "ClientId is required"));
            }
            if (clientOrder.DeliveryId == default(int))
            {
                errors.Add(new KeyValuePair<string, string>("DeliveryId", "DeliveryId is required"));
            }
            if (clientOrder.Orders == null || clientOrder.Orders.Count == 0)
            {
                errors.Add(new KeyValuePair<string, string>("ProductId", "ProductId is required"));
            }
            if (clientOrder.Orders != null && clientOrder.Orders.Count > 0 && clientOrder.Orders.Any(x => x.ProductId == default(int)))
            {
                errors.Add(new KeyValuePair<string, string>("ProductId", "Quantity must be a positive number"));
            }
            if (clientOrder.Orders != null && clientOrder.Orders.Count > 0 && clientOrder.Orders.Any(x => x.Quantity <= 0))
            {
                errors.Add(new KeyValuePair<string, string>("Quantity", "Quantity must be a positive number"));
            }

            return errors;
        }

        public List<KeyValuePair<string, string>> CanUpdateClientOrder( ClientOrder clientOrder ) {
            if( clientOrder == null ) {
                throw new ArgumentNullException( "ClientOrder can not be null" );
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(clientOrder.ClientId))
            {
                errors.Add(new KeyValuePair<string, string>("ClientId", "ClientId is required"));
            }
            if (clientOrder.DeliveryId == default(int))
            {
                errors.Add(new KeyValuePair<string, string>("DeliveryId", "DeliveryId is required"));
            }
            if (clientOrder.Orders == null || clientOrder.Orders.Count == 0)
            {
                errors.Add(new KeyValuePair<string, string>("ProductId", "ProductId is required"));
            }
            if (clientOrder.Orders != null && clientOrder.Orders.Count > 0 && clientOrder.Orders.Any(x => x.ProductId == default(int)))
            {
                errors.Add(new KeyValuePair<string, string>("ProductId", "Quantity must be a positive number"));
            }
            if (clientOrder.Orders != null && clientOrder.Orders.Count > 0 && clientOrder.Orders.Any(x => x.Quantity <= 0))
            {
                errors.Add(new KeyValuePair<string, string>("Quantity", "Quantity must be a positive number"));
            }

            return errors;
        }
    }
}