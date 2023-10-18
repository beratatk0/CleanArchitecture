using Clean.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var CustomResponse = await _service.GetProductsWithCategory();
            return View(CustomResponse.Data);
        }
    }
}
