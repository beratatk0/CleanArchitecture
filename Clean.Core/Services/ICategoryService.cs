using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        public Task<CustomResponseDTO<CategoryWithProductsDTO>> GetCategoryWithProductsById(int categoryId);
    }
}
