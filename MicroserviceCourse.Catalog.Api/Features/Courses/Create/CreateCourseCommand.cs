namespace MicroserviceCourse.Catalog.Api.Features.Courses.Create
{
    /// <summary>
    /// Course endpointinin oluşturulmasındaki modelimiz. Geriye sadece oluşturulan kursun Guid'ini dönecek.
    /// </summary>
    public record class CreateCourseCommand(
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId) : IRequestByServiceResult<Guid>;
}
