using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table( "Orders" )]
    public class Order
    {
        [Key]
        [Column( "Id" )]
        public int Id { get; set; }

        public int ProductId { set; get; }
        [ForeignKey( "ProductId" )]
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } // sum of all product price

        public int ClientOrderId { set; get; }
        [ForeignKey( "ClientOrderId" )]
        public virtual ClientOrder ClientOrder { get; set; }
    }
}