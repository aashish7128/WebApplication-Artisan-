using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Artisan_.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Category")]
        public required string Name { get; set; }
    }
}
