using System.ComponentModel.DataAnnotations;

namespace FandMHandicrafts.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
