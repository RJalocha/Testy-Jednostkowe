using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table( "Products" )]
    public class Product
    {
        [Key]
        [Column( "Id" )]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public virtual ICollection<Rate> Rates { get; set; }
    }
}