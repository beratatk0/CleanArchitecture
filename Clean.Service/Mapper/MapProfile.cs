﻿using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Service.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDTO>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<Product, ProductWithCategoryDTO>();
            CreateMap<Category, CategoryWithProductsDTO>();
            CreateMap<ProductCreateDTO, ProductDTO>();
            CreateMap<ProductCreateDTO, Product>();
        }
    }
}
