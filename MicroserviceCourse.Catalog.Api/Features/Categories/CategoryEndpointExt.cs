using Asp.Versioning.Builder;
using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Features.Categories.GetAll;
using MicroserviceCourse.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceCourse.Catalog.Api.Features.Categories
{
    /// <summary>
    /// Bu classın amacı CategoryEndpoint sınıfını gruplayıp genişletmektir.
    /// </summary>
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();

        }
    }
}
