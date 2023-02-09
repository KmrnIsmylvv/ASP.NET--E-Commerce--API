using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Address = Core.Entities.Identity.Address;

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
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderReturnDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}