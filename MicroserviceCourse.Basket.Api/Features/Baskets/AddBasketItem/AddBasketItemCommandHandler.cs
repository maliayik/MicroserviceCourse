using MediatR;
using MicroserviceCourse.Basket.Api.Data;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Services;
using System.Text.Json;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService,BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var existingItem = currentBasket.Items.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingItem is not null)
            {
                // TODO : ihtiyaca göre business kuralları eklenebilir.
                currentBasket.Items.Remove(existingItem);
            }

            currentBasket.Items.Add(newBasketItem);

            //sepette indirim kodu varsa eklenen ürüne de uygulanmalı
            currentBasket.ApplyAvailableDiscount();

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
