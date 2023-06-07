using Fiorello.Areas.Admin.ViewModels.Product;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
           var products=await _productService.GetAllWithIncludesAsync();

            return View(_productService.GetMappedDatas(products));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int ? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetWithIncludesAsync(id);

            if (product == null) return NotFound();

            return View(_productService.GetMappedData(product));


        }
    }
}
