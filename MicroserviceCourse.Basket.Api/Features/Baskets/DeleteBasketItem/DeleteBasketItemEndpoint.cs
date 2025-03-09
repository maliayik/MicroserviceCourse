using MediatR;
using MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:Guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);             

            return group;
        }
    }
}
