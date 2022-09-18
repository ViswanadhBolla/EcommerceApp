using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EcommerceAppWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(45, ErrorMessage = "The category name can be maximum 45 characters long")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
