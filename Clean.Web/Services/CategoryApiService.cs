using Clean.Core.DTOs;
using System.Text.Json;

namespace Clean.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CategoryDTO>>>("categories");
            return response.Data;
        }

        //public async Task<CategoryWithProductsDTO> GetCategoryWithProductsById(int id)
        //{
        //    var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<CategoryWithProductsDTO>>($"categories/GetCategoryWithProductsById/{id}");
        //    return response.Data;
        //}
    }
}
