using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.Create;

namespace MicroserviceCourse.Catalog.Api.Features.Courses
{
    /// <summary>
    /// Bu classın amacı CourseEndpointExt sınıfını gruplayıp genişletmektir.
    /// </summary>
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses").CreateCourseGroupItemEndpoint();
        }
    }
}
