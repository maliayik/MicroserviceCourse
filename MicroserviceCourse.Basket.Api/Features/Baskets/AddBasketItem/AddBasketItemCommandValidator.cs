using FluentValidation;

namespace MicroserviceCourse.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator:AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("{PropertyName} is required"); ;
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithName("{PropertyName} must be greater than zero");
        }
    }
}
