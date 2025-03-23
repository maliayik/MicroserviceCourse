using MediatR;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceCourse.File.Api.Features.File.Delete;

public static class DeleteFileCommandEndpoint
{
    public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("",
                async ([FromBody] DeleteFileCommand deleteFileCommand, IMediator mediator) =>
                (await mediator.Send(deleteFileCommand)).ToGenericResult())
            .WithName("delete")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created);

        return group;
    }
}