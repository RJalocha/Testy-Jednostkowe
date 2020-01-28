using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.Helpers
{
    public interface IOrderTotalPriceCalculator
    {
        decimal Calculate(Order order);
    }

    public class OrderTotalPriceCalculator : IOrderTotalPriceCalculator
    {
        public decimal Calculate(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("Order can not be null");
            }

            decimal result = 0;

            result += order.Product.Price * order.Quantity;

            return result;
        }
    }
}