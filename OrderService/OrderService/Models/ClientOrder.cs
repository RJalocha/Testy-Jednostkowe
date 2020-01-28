using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table( "ClientOrders" )]
    public class ClientOrder
    {
        [Key]
        [Column( "Id" )]
        public int Id { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public decimal? Discount { get; set; }
        public decimal TotalPrice { get; set; } // Orders.TotalPrice + Deliery.Price + Discount

        public string ClientId { set; get; }
        [ForeignKey( "ClientId" )]
        public virtual ApplicationUser Client { get; set; }

        public int DeliveryId { set; get; }
        [ForeignKey( "DeliveryId" )]
        public virtual Delivery Delivery { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}