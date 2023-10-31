using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Services;
using Clean.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly ProductApiService _productApiService;


        public ProductController(IProductService service, ICategoryService categoryService, IMapper mapper, CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoryDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var category = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(category, "Id", "Name");
            return View();

        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);

        }

        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}
