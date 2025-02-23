using MediatR;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kategori oluşturmak için kullanılır.
        /// </summary>

        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult());

            return group;
        }
    }
}
