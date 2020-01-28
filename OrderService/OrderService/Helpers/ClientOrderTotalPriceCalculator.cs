using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.Helpers
{
    public interface IClientOrderTotalPriceCalculator
    {
        decimal Calculate(ClientOrder clientOrder);
    }

    public class ClientOrderTotalPriceCalculator : IClientOrderTotalPriceCalculator
    {
        public decimal Calculate(ClientOrder clientOrder)
        {
            if (clientOrder == null)
            {
                throw new ArgumentNullException("ClientOrder can not be null");
            }
            if (clientOrder.Orders == null)
            {
                throw new ArgumentNullException("Orders can not be null");
            }

            decimal result = 0;

            foreach(Order order in clientOrder.Orders)
            {
                result += order.Product.Price * order.Quantity;
            }

            result += clientOrder.Delivery.Price;
            result -= (clientOrder.Discount.HasValue ? clientOrder.Discount.Value : 0m);

            return result;
        }
    }
}