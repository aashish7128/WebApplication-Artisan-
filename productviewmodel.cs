using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_Artisan_.Models;

namespace WebApplication_Artisan_
{
    public class productviewmodel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the product price.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please enter the product quantity.")]
        public int Quantity { get; set; }

        public string Description { get; set; }

        public IFormFile photo { get; set; }


        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }
    }
}
