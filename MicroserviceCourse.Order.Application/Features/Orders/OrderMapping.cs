using AutoMapper;
using MicroserviceCourse.Order.Application.Features.Orders.CreateOrders;

namespace MicroserviceCourse.Order.Application.Features.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Domain.Entities.OrderItem, OrderItemDto>().ReverseMap();
    }
}