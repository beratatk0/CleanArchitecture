using AutoMapper;
using Clean.API.Filters;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Clean.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDTO>>(products.ToList());
            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDTO>(product);
            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productsDto));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var items = await _service.GetProductsWithCategory();
            return CreateActionResult(items);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {
            await _service.AddAsync(_mapper.Map<Product>(product));
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(201));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO product)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(product));
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
            {
                return CreateActionResult(CustomResponseDTO<NoContentDTO>.Fail(404, "No products found with this id"));
            }
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }

    }
}
