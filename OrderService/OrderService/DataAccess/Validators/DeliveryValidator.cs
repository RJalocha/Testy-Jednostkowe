using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Validators
{
    public interface IDeliveryValidator
    {
        List<KeyValuePair<string, string>> CanAddDelivery(Delivery delivery);
        List<KeyValuePair<string, string>> CanUpdateDelivery(Delivery delivery);
    }

    public class DeliveryValidator : IDeliveryValidator
    {
        public List<KeyValuePair<string, string>> CanAddDelivery(Delivery delivery)
        {
            if(delivery == null)
            {
                throw new ArgumentNullException("Delivery can not be null");
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(delivery.Name))
            {
                errors.Add(new KeyValuePair<string, string>("Name", "Name is required"));
            }
            if (delivery.Price <= 0)
            {
                errors.Add(new KeyValuePair<string, string>("Price", "Price must be positive number"));
            }
            if (delivery.DeliveryDays < 0)
            {
                errors.Add(new KeyValuePair<string, string>("DeliveryDays", "DeliveryDays must be non negative number"));
            }

            return errors;
        }

        public List<KeyValuePair<string, string>> CanUpdateDelivery(Delivery delivery)
        {
            if (delivery == null)
            {
                throw new ArgumentNullException("Delivery can not be null");
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(delivery.Name))
            {
                errors.Add(new KeyValuePair<string, string>("Name", "Name is required"));
            }
            if (delivery.Price <= 0)
            {
                errors.Add(new KeyValuePair<string, string>("Price", "Price must be positive number"));
            }

            return errors;
        }
    }
}