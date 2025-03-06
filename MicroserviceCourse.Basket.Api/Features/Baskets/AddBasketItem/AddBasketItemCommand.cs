using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem
{
    public record class AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string ImageUrl) : IRequestByServiceResult;
}
