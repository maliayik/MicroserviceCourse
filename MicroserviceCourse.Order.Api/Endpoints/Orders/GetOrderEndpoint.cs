using MediatR;
using MicroserviceCourse.Order.Application.Features.Orders.CreateOrders;
using MicroserviceCourse.Order.Application.Features.Orders.GetOrders;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Order.Api.Endpoints.Orders;

public static class GetOrdersEndpoint
{
    public static RouteGroupBuilder GetOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/",
                async (IMediator mediator) =>
                    (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
            .WithName("GetOrders")
            .MapToApiVersion(1, 0);
            

        return group;
    }
}