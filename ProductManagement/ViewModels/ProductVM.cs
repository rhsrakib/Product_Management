using ProductManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        
        public int? CustomerId { get; set; }

        public string? ProductName { get; set; } = string.Empty;

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; } = decimal.Zero;

        public decimal? TotalPrice { get; set; } = decimal.Zero;
    }
}