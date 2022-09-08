﻿using API.DTOs;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.ProductBrand, opt =>
                    opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt =>
                    opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt =>
                    opt.MapFrom<ProductUrlResolver>());
        }
    }
}