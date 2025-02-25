namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create
{
    //Command ile bitenler Create, update, insert , delete işlemleri için kullanılır.
    //record olarak tanımladık, çünkü dışarıdan immutability (değiştirilemez) olmasını istiyoruz.

    public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;

}
