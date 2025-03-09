using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
