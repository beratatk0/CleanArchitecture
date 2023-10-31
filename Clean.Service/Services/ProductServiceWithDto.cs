using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDTO>, IProductServiceWithDto
    {
        private readonly IProductRepository _repository;
        public ProductServiceWithDto(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository, IGenericRepository<Product> repository) : base(mapper, unitOfWork, repository)
        {
            _repository = productRepository;
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var productsWithCategory = await _repository.GetProductsWithCategory();
            var products = _mapper.Map<List<ProductWithCategoryDTO>>(productsWithCategory);
            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(StatusCodes.Status200OK, products);
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto)
        {
            var newProduct = _mapper.Map<Product>(dto);
            await _repository.AddAsync(newProduct);
            await _unitOfWork.CommitASync();
            var newDto = _mapper.Map<ProductDTO>(dto);
            return CustomResponseDTO<ProductDTO>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDTO<List<ProductDTO>>> AddRangeAsync(List<ProductCreateDTO> dtos)
        {
            var newProducts = _mapper.Map<List<Product>>(dtos);
            await _repository.AddRangeAsync(newProducts);
            await _unitOfWork.CommitASync();
            var newDtos = _mapper.Map<List<ProductDTO>>(dtos);
            return CustomResponseDTO<List<ProductDTO>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            _repository.Update(product);
            await _unitOfWork.CommitASync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }
    }
}
