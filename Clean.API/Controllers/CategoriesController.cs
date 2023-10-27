using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoryWithProductsById(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetCategoryWithProductsById(categoryId));
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            return CreateActionResult(CustomResponseDTO<List<CategoryDTO>>.Success(200, categoryDtos));
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var category = await _service.GetByIdAsync(id);
        //    var categoryDto = _mapper.Map<CategoryDTO>(category);
        //    return CreateActionResult(CustomResponseDTO<CategoryDTO>.Success(200, categoryDto));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Save(CategoryDTO categoryDto)
        //{
        //    await _service.AddAsync(_mapper.Map<Category>(categoryDto));
        //    return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(CategoryDTO categoryDto)
        //{
        //    await _service.UpdateAsync(_mapper.Map<Category>(categoryDto));
        //    return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = await _service.GetByIdAsync(id);
        //    if (category == null)
        //    {
        //        return CreateActionResult(CustomResponseDTO<NoContentDTO>.Fail(404, "No category found with this id"));
        //    }
        //    await _service.RemoveAsync(category);
        //    return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        //}

    }
}
