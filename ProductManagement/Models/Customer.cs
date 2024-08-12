using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }


        [Required, Display(Name = "Order No")]
        [Column(TypeName = "nvarchar(50)")]
        public string? OrderNo { get; set; } = string.Empty;


        [Required(ErrorMessage = "Customer name is Required."), Display(Name = "Customer Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Phone No is Required.")]
        [Column(TypeName = "nvarchar(14)")]
        public string Phone { get; set; } = string.Empty;


        [Required(ErrorMessage = "Address is Required.")]
        [Column(TypeName = "nvarchar(max)")]
        public string Address { get; set; } = string.Empty;


        [Required, Display(Name = "Total Order Quantity")]
        [Column(TypeName = "int")]
        public int? TotalOrderQty { get; set; } 


        [Required, Display(Name = "Total Order Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalOrderPrice { get; set; } = decimal.Zero;


        public IList<Product>? Products { get; set; } = new List<Product>();
    }
}
