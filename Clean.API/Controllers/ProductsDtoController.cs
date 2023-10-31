using AutoMapper;
using Clean.API.Filters;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers
{
    public class ProductsDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _service;
        private readonly IMapper _mapper;

        public ProductsDtoController(IMapper mapper, IProductServiceWithDto service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDTO product)
        {
            return CreateActionResult(await _service.AddAsync(product));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO product)
        {
            return CreateActionResult(await _service.UpdateAsync(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }

        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<ProductCreateDTO> products)
        {
            return CreateActionResult(await _service.AddRangeAsync(products));
        }
        [HttpDelete("RemoveRange")]
        public async Task<IActionResult> DeleteRange(List<int> ids)
        {
            return CreateActionResult(await _service.RemoveRangeAsync(ids));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _service.AnyAsync(x => x.Id == id));
        }
    }
}
