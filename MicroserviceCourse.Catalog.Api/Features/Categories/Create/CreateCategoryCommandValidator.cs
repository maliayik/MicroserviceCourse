namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{ProdpertyName} connot be empty")
                .Length(4,25).WithMessage("{PropertyName} must be between 4 and 25 characters");
        }
    }
}
