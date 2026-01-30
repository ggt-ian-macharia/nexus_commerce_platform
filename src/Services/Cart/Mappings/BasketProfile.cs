using AutoMapper;
using Cart.DTOs;
using Cart.Models;

namespace Cart.Mappings;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        // ShoppingCart mappings
        CreateMap<ShoppingCart, BasketResponse>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

        CreateMap<UpdateBasketRequest, ShoppingCart>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // ShoppingCartItem mappings
        CreateMap<ShoppingCartItem, BasketItemResponse>()
            .ForMember(dest => dest.ItemTotal, opt => opt.MapFrom(src => src.Price * src.Quantity));

        CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
    }
}
