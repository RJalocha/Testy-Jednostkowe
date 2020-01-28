using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table( "Deliveries" )]
    public class Delivery
    {
        [Key]
        [Column( "Id" )]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DeliveryDays { get; set; }

        public virtual ICollection<ClientOrder> ClientOrders { get; set; }
    }
}