using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Artisan_.Models
{
    public class Craft
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the craft name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the craft price.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter the craft quantity.")]
        public int Quantity { get; set; }

        public string Description { get; set; }

        // Image attribute using IFormFile to handle file uploads
        [Display(Name = "Craft Image")]
        public IFormFile Image { get; set; }

        // Boolean indicating if the craft is considered a "TopCraft"
        [Display(Name = "Top Craft")]
        public bool TopCraft
        {
            get { return Price > 500; }
        }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }
    }
}
