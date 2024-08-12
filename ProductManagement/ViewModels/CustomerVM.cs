using ProductManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }

        public string? OrderNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer name is Required.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone No is Required.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is Required.")]
        public string Address { get; set; } = string.Empty;

        public int? TotalOrderQty { get; set; }

        public decimal? TotalOrderPrice { get; set; } = decimal.Zero;

        // Product
        public int ProductId { get; set; }

        public string? ProductName { get; set; } = string.Empty;

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; } = decimal.Zero;

        public decimal? TotalPrice { get; set; } = decimal.Zero;

        public IList<ProductVM>? Products { get; set; } = new List<ProductVM>();
    }
}
