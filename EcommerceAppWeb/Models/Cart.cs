using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAppWeb.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string CartId { get; set; }

        public int ProductId { get; set; }
        public int Count { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }
    }
}
