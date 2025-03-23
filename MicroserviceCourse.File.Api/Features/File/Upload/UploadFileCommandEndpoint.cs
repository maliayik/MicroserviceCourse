using MediatR;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.File.Api.Features.File.Upload
{
    public static class UploadFileCommandEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kategori oluşturmak için kullanılır.
        /// </summary>
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (IFormFile file, IMediator mediator) =>
                        (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
                .WithName("upload")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status201Created).DisableAntiforgery();

            return group;
        }
    }
}