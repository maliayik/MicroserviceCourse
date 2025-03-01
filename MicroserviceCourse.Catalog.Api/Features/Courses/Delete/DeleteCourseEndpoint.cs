using MicroserviceCourse.Catalog.Api.Features.Courses.Update;
using MicroserviceCourse.Shared.Filters;

namespace MicroserviceCourse.Catalog.Api.Features.Courses.Delete
{
    //istersek update işlemi için yaptıgımız gibi farklı classlar oluşturabiliriz.Burada tek bir sınıf içerisinde işlemi gerçekleştirdik.
    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

    public class DeleteCourseHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);

            if (hasCourse is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            context.Courses.Remove(hasCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .WithName("DeleteCourse");

            return group;
        }
    }
}
