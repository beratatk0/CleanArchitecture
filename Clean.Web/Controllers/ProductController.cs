using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, ICategoryService categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var CustomResponse = await _service.GetProductsWithCategory();
            return View(CustomResponse.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var category = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDTO>>(category.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }
            var category = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDTO>>(category.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();

        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDTO>(product);
            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDTO>>(categories);
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", product.CategoryId);

            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDTO>>(categories);
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", product.CategoryId);
            return View(product);

        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}
