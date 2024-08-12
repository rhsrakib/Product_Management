using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.ViewModels;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        #region Get
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var vmCustomer = new CustomerVM();
            if (id.HasValue && id > 0)
            {
                var customer = await _context.Customers.Include(x => x.Products).FirstOrDefaultAsync(x => x.CustomerId == id);
                if(customer != null)
                {
                    vmCustomer.CustomerId = customer.CustomerId;
                    vmCustomer.OrderNo = customer.OrderNo;
                    vmCustomer.CustomerName = customer.CustomerName;
                    vmCustomer.Phone = customer.Phone;
                    vmCustomer.Address = customer.Address;
                    vmCustomer.TotalOrderQty = customer.TotalOrderQty;
                    vmCustomer.TotalOrderPrice = customer.TotalOrderPrice;


                    var vmProduct = new List<ProductVM>();
                    var listProduct = _context.Products.Where(x => x.CustomerId == vmCustomer.CustomerId).ToList();
                    foreach (var product in listProduct)
                    {
                        var newProduct = new ProductVM();
                        newProduct.ProductId = product.ProductId;
                        newProduct.CustomerId = product.CustomerId;
                        newProduct.ProductName = product.ProductName;
                        newProduct.Quantity = product.Quantity;
                        newProduct.UnitPrice = product.UnitPrice;
                        newProduct.TotalPrice = product.TotalPrice;
                        //newProduct.TotalOrderQty = product.TotalOrderQty;
                        //newProduct.TotalOrderPrice = product.TotalOrderPrice;
                        vmProduct.Add(newProduct);
                    }
                    vmCustomer.Products = vmProduct;
                }
            }
            ViewData["CustomerList"] = _context.Customers.ToList();
            ViewData["ProductList"] = _context.Products.ToList();
            return View(vmCustomer);
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM vmCustomer)
        {
            var customer = await _context.Customers
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.CustomerId == vmCustomer.CustomerId);
            if (customer == null)
            {
                customer = new Customer();
                customer.CustomerId = vmCustomer.CustomerId;
                customer.OrderNo = vmCustomer.OrderNo;
                customer.CustomerName = vmCustomer.CustomerName;
                customer.Phone = vmCustomer.Phone;
                customer.Address = vmCustomer.Address;
                customer.TotalOrderQty = 0;
                customer.TotalOrderPrice = 0;
                await _context.Customers.AddAsync(customer);
            }
            else
            {
                customer.OrderNo = vmCustomer.OrderNo;
                customer.CustomerName = vmCustomer.CustomerName;
                customer.Phone = vmCustomer.Phone;
                customer.Address = vmCustomer.Address;
            }
            // Handle products
            var existingProducts = customer.Products.ToList();

            // Remove products that are not in the new list
            var removedProducts = existingProducts.Where(p => !vmCustomer.Products.Any(x => x.ProductId == p.ProductId)).ToList();
            _context.Products.RemoveRange(removedProducts);

            if (vmCustomer.Products != null && vmCustomer.Products.Any())
            {
                // Update or add products
                foreach (var item in vmCustomer.Products)
                {
                    var existingProduct = existingProducts.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (existingProduct != null)
                    {
                        existingProduct.ProductName = item.ProductName;
                        existingProduct.Quantity = item.Quantity;
                        existingProduct.UnitPrice = item.UnitPrice;
                        existingProduct.TotalPrice = item.Quantity * item.UnitPrice;
                    }
                    else
                    {
                        var newProduct = new Product
                        {
                            CustomerId = customer.CustomerId,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.Quantity * item.UnitPrice
                        };
                        customer.Products.Add(newProduct);
                    }

                    customer.TotalOrderQty = vmCustomer.Products.Sum(x => x.Quantity ?? 0);
                    customer.TotalOrderPrice = vmCustomer.Products.Sum(x => x.Quantity * x.UnitPrice ?? 0);
                }

                await _context.SaveChangesAsync();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ViewData["CustomerList"] = _context.Customers.Include(x => x.Products).ToList();
                return PartialView("_CustomerList", _context.Customers.Include(x => x.Products).ToList());
            }
            return RedirectToAction("Index");
        }
        #endregion


        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer ==  null)
            {
                return NotFound();
            }
            var product = _context.Products.Where(x => x.CustomerId == customer.CustomerId);
            if(product != null)
            {
                _context.Products.RemoveRange(product);
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return PartialView("_CustomerList", _context.Customers.ToList());
        }
        #endregion


        #region Generate Auto Increment Order Code
        [HttpGet]
        public IActionResult GenerateNextOrderNo()
        {
            var lastOrder = _context.Customers.Max(x => x.OrderNo);
            string nextOrder = "ORDER00001";
            if(!string.IsNullOrEmpty(lastOrder))
            {
                int lastNumber = int.Parse(lastOrder.Substring(5));
                lastNumber++;
                nextOrder = $"ORDER{lastNumber:D5}";
            }
            return Json(nextOrder);
        }
        #endregion


        #region Generate PDF Report Get
        [HttpGet]
        public IActionResult GetCustomerProductData(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Products) // Ensure that Products is a valid navigation property
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            var result = new
            {
                customer.CustomerName,
                customer.Phone,
                customer.Address,
                Products = customer.Products.Select(p => new
                {
                    p.ProductName,
                    p.Quantity,
                    p.UnitPrice,
                    TotalPrice = p.Quantity * p.UnitPrice
                }).ToList()
            };

            return Json(result);
        }
        #endregion
    }
}
