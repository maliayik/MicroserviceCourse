using MicroserviceCourse.Discount.Api.Features.Discounts.GetDiscountByCode;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class GetDiscountByCodeEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kategori oluşturmak için kullanılır.
        /// </summary>
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{code:length(10)}", async (string code, IMediator mediator) => (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
                .WithName("GetDiscountByCode")
                .MapToApiVersion(1, 0)
                .Produces<GetDiscountByCodeQueryResponse>(StatusCodes.Status200OK);
            return group;
        }
    }
}
