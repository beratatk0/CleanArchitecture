using Clean.Core.DTOs;
using Clean.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory();

        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto);

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto);

    }
}
