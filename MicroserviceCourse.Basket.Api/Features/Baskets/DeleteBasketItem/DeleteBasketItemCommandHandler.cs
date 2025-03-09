﻿using System.Net;
using System.Text.Json;
using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userID = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userID);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketİtemToDelete = currentBasket!.Items.FirstOrDefault(x => x.Id == request.Id);

            if (basketİtemToDelete is null)
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            currentBasket.Items.Remove(basketİtemToDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
