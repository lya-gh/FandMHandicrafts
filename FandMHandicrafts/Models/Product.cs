using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FandMHandicrafts.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; } 

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Product Image is required.")]
        public string ProductImage { get; set; } 

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description is required.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price is required.")]
        public double ProductPrice { get; set; }

        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Product Category is required.")]
        public ProductCategory ProductCategory { get; set; }
    }
}
