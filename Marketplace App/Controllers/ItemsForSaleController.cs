using Marketplace_App.Data;
using Marketplace_App.Models;
using Marketplace_App.VM;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_App.Controllers
{
    public class ItemsForSaleController : Controller
    {
        private readonly MarketplaceContext _marketplaceContext;
        public ItemsForSaleController(MarketplaceContext marketplaceContext)
        {
            _marketplaceContext = marketplaceContext;
        }
        public IActionResult ItemsForSale()
        {
            ItemsForSaleVM vm = new ItemsForSaleVM();
            vm.Products = _marketplaceContext.products.ToList();
            return View(vm);
        }
        public IActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductVM product)
        {
            if (product != null)
            {
                Product rtn = new Product
                {
                    Description = product.Description,
                    Name = product.Name,
                    ID = product.ID,
                    Price = Convert.ToInt32(product.Price),
                    Image = product.Image
                };
                _marketplaceContext.products.Add(rtn);
                _marketplaceContext.SaveChanges();
                return RedirectToAction("ItemsForSale");
            }
            return View(product);
        }
    }
}
