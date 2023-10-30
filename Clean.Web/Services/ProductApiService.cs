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
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDTO>>>("ProductsDto/GetProductsWithCategory");
            return response.Data;
        }
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"ProductsDto/{id}");
            return response.Data;
        }
        public async Task<ProductDTO> SaveAsync(ProductDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync("ProductsDto", product);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();
            return responseBody.Data;

        }
        public async Task<bool> UpdateAsync(ProductDTO product)
        {
            var response = await _httpClient.PutAsJsonAsync("ProductsDto", product);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"ProductsDto/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
