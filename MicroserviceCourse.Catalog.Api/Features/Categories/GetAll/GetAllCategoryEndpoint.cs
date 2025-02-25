﻿using MediatR;
using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Features.Categories.Dtos;
using MicroserviceCourse.Catalog.Api.Repositories;
using MicroserviceCourse.Shared;
using MicroserviceCourse.Shared.Extensions;
using MicroserviceCourse.Shared.Filters;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Catalog.Api.Features.Categories.GetAll
{

    public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;

    public class GetAllCategoryQueryHandler(AppDbContext context)
        : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            var categoryDtos = categories.Select(x => new CategoryDto(x.Id, x.Name)).ToList();

            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoryDtos);
        }
    }


    public static class GetAllCategoryEndpoint
    {
        /// <summary>
        /// Bu endpoint, bir kategorileri getirmek için kullanılır.
        /// </summary>

        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

            return group;
        }
    }
}
