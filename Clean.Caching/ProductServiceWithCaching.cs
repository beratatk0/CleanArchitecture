using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Core.UnitOfWorks;
using Clean.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Caching
{
    public class ProductServiceWithCaching : IProductService
    {

        private readonly string CachingKey = "productsCache";
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;



        public ProductServiceWithCaching(IUnitOfWork unitOfWork, IProductRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _cache = cache;

            if (!_cache.TryGetValue(CachingKey, out _))
            {
                _cache.Set(CachingKey, _repository.GetProductsWithCategory().Result);
            }
        }


        public async Task<Product> AddAsync(Product item)
        {
            await _repository.AddAsync(item);
            await _unitOfWork.CommitASync();
            await CacheAllProductsAsync();
            return item;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> items)
        {
            await _repository.AddRangeAsync(items);
            await _unitOfWork.CommitASync();
            await CacheAllProductsAsync();
            return items;
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            //var products = _cache.Get<IEnumerable<Product>>(CachingKey);
            //return products.Any(expression.Compile());
            return await _repository.AnyAsync(expression);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = _cache.Get<IEnumerable<Product>>(CachingKey);

            return Task.FromResult(products);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _cache.Get<List<Product>>(CachingKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }
            return Task.FromResult(product);
        }

        public Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var products = _cache.Get<IEnumerable<Product>>(CachingKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return Task.FromResult(CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsWithCategoryDto));
        }

        public async Task RemoveAsync(Product item)
        {
            _repository.Remove(item);
            await _unitOfWork.CommitASync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> items)
        {
            _repository.RemoveRange(items);
            await _unitOfWork.CommitASync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            _repository.Update(item);
            await _unitOfWork.CommitASync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _cache.Get<List<Product>>(CachingKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProductsAsync()
        {
            await _cache.Set(CachingKey, _repository.GetAll().ToListAsync());
        }
    }
}
