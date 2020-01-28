using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    [Table( "Rates" )]
    public class Rate
    {
        [Key]
        [Column( "Id" )]
        public int Id { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }

        public int ProductId { set; get; }
        [ForeignKey( "ProductId" )]
        public virtual Product Product { get; set; }

        public string RatingUserId { set; get; }
        [ForeignKey( "RatingUserId" )]
        public virtual ApplicationUser RatingUser { get; set; }
    }
}