using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }


        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        [Required(ErrorMessage = "Product name is Required."), Display(Name = "Product Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string? ProductName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Quantity is Required.")]
        [Column(TypeName = "nvarchar(100)")]
        public int? Quantity { get; set; }


        [Required(ErrorMessage = "Unit price is Required."), Display(Name = "Unit Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitPrice { get; set; } = decimal.Zero;

        [Required, Display(Name = "Total Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPrice { get; set; } = decimal.Zero;
    }
}
