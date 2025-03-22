
using MicroserviceCourse.Discount.Api.Repositories;
using MicroserviceCourse.Shared.Services;

namespace MicroserviceCourse.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context,IIdentityService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = new Repositories.Discount()
            {
               Id = NewId.NextSequentialGuid(),
               Code= request.Code,
               Created = DateTime.Now,
               Rate = request.Rate,
               Expired = request.Expired,
               UserId = identityService.GetUserId
            };

            await context.Discounts.AddAsync(discount,cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
