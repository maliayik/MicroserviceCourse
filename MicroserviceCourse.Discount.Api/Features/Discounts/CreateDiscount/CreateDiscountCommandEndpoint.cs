using Asp.Versioning.Builder;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountCommandEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kategori oluşturmak için kullanılır.
        /// </summary>
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}
