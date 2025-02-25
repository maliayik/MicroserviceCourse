namespace MicroserviceCourse.Catalog.Api.Features.Categories.Create
{
    //Eğer ORM aracımızı değiştirmek istersek repository design pattern kullanmamız gerekir.
    //Bu kodumuzda EFCore orm aracını değiştirmiyeceğimiz için handler sınıfında dbcontext kullanabiliriz.
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory =
                await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

            if (existCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error("Category name already exist",
                    $"The category name '{request.Name}' already exist", HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid() //indexleme daha verimli olması için mastransit üzerinden NextSequentialGuid artan guid oluşturduk.
            };


            await context.AddAsync(category, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "CreateCategoryGroupItemEndpoint");
        }
    }
}
