using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Filters;
using MicroserviceCourse.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using MicroserviceCourse.Shared.Extensions;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;

    public class RemoveDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            basket!.ClearDiscount();

            basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",
                    async (IMediator mediator) =>
                        (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
