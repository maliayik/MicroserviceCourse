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
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint();

        }
    }
}
