using Amazon.Runtime.Internal;
using MediatR;
using MicroserviceCourse.Shared;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create
{
    //Command ile bitenler Create, update, insert , delete işlemleri için kullanılır.
    //record olarak tanımladık, çünkü dışarıdan immutability (değiştirilemez) olmasını istiyoruz.

    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;

}
