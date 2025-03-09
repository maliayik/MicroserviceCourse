using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MicroserviceCourse.Shared.Services;
using MicroserviceCourse.Basket.Api.Data;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userID = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userID);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(userID, [newBasketItem]);
                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var existingItem = currentBasket.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingItem is not null)
            {
                // TODO : ihtiyaca göre business kuralları eklenebilir.
                currentBasket.Items.Remove(existingItem);
            }

            currentBasket.Items.Add(newBasketItem);

            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        // Yardımcı metot. Cache oluştururken kullanılır.
        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }
    }
}
