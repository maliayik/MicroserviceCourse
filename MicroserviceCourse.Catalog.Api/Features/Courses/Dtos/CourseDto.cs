namespace MicroserviceCourse.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Name, decimal price, string Description, string ImageUrl, CategoryDto Category, FeatureDto Feature);
}
