namespace MicroserviceCourse.Basket.Api.Dtos
{
    public record BasketDto(Guid userId, List<BasketItemDto> BasketItems);
}
