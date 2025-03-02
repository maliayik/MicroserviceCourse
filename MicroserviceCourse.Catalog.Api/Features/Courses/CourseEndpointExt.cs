using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.Create;
using MicroserviceCourse.Catalog.Api.Features.Courses.Delete;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetAll;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetAllByUserId;
using MicroserviceCourse.Catalog.Api.Features.Courses.GetById;
using MicroserviceCourse.Catalog.Api.Features.Courses.Update;

namespace MicroserviceCourse.Catalog.Api.Features.Courses
{
    /// <summary>
    /// Bu classın amacı CourseEndpointExt sınıfını gruplayıp genişletmektir.
    /// </summary>
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint()
                .GetByIdCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetByUserIdCourseGroupItemEndpoint();
        }
    }
}
