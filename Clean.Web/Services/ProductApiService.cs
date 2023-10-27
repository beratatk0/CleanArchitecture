using Clean.Core.DTOs;

namespace Clean.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategory()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDTO>>>("Products/GetProductsWithCategory");
            return response.Data;
        }
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"Products/{id}");
            return response.Data;
        }
        public async Task<ProductDTO> SaveAsync(ProductDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", product);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();
            return responseBody.Data;

        }
        public async Task<bool> UpdateAsync(ProductDTO product)
        {
            var response = await _httpClient.PutAsJsonAsync("Products", product);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
