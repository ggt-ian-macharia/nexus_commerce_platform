using AutoMapper;
using Order.DTOs;
using Order.Models;

namespace Order.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Order mappings
        CreateMap<Models.Order, OrderResponse>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

        CreateMap<CreateOrderRequest, Models.Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // OrderItem mappings
        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));

        CreateMap<CreateOrderItemRequest, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());
    }
}
