using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kurs oluşturmak için kullanılır.
        /// </summary>
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult()).WithName("CreateCourse")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}

