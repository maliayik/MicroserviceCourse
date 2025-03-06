using MediatR;
using MicroserviceCourse.Basket.Api.Const;
using MicroserviceCourse.Basket.Api.Dtos;
using MicroserviceCourse.Shared;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            // TODO : daha sonra token tarafından gelen kullanıcı id alınacak
            Guid userID = Guid.NewGuid();

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userID);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            BasketDto? currentBasket;

            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new BasketDto(userID, [newBasketItem]);
                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var existingItem = currentBasket.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingItem is not null)
            {
                currentBasket.BasketItems.Remove(existingItem);
            }

            currentBasket.BasketItems.Add(newBasketItem);

            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        // Yardımcı metot. Cache oluştururken kullanılır.
        private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }
    }
}
