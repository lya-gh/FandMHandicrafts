using System.ComponentModel.DataAnnotations;

namespace FandMHandicrafts.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order order { get; set; }


    }
}
