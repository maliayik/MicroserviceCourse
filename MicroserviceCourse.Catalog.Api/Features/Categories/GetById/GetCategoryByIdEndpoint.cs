using System.Net;
using AutoMapper;
using MediatR;
using MicroserviceCourse.Catalog.Api.Features.Categories.Dtos;
using MicroserviceCourse.Catalog.Api.Features.Categories.GetAll;
using MicroserviceCourse.Catalog.Api.Repositories;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;

    public class GetCategoryByIdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);

            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Error("Category not found", $"The Category with Id ({request.Id}) was not found", HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryDto>(hasCategory);
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        /// <summary>
        /// Bu endpoint, Id ye göre  kategorileri getirmek için kullanılır.
        /// </summary>

        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //burada id nin guid olması gerektiğini belirtiyoruz bu sayede requesti güvenli hale getirmiş oluyoruz ekstra validasyon yapmamıza gerek kalmıyor. (.Net guid hariç değerleri kabul etmiyor.)
            group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) => (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());

            return group;
        }
    }


}
