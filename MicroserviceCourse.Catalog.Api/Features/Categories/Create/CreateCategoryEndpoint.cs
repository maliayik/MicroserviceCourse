using MediatR;
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
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return new ObjectResult(result)
                {
                    StatusCode = result.StatusCode.GetHashCode()
                };
            });

            return group;
        }
       
    }
}
