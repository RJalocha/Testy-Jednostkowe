using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.Helpers
{
    public interface IClientOrderDeliveryDateCalculator
    {
        DateTime Calculate(ClientOrder clientOrder);
    }

    public class ClientOrderDeliveryDateCalculator : IClientOrderDeliveryDateCalculator
    {
        public DateTime Calculate(ClientOrder clientOrder)
        {
            if (clientOrder == null)
            {
                throw new ArgumentNullException("ClientOrder can not be null");
            }

            DateTime result = clientOrder.CreationDate.AddDays(clientOrder.Delivery.DeliveryDays);

            for(int i=0; i< clientOrder.Delivery.DeliveryDays; i++)
            {
                if(clientOrder.CreationDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday ||
                    clientOrder.CreationDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    result = result.AddDays(1);
                }
            }

            return result;
        }
    }
}