using Microsoft.AspNetCore.Http; // Add this namespace
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Artisan_.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the product price.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter the product quantity.")]
        public int Quantity { get; set; }

        public string Description { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }

        
    }
}
