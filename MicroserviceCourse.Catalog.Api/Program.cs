using MicroserviceCourse.Catalog.Api;
using MicroserviceCourse.Catalog.Api.Features.Categories;
using MicroserviceCourse.Catalog.Api.Features.Courses;
using MicroserviceCourse.Catalog.Api.Options;
using MicroserviceCourse.Catalog.Api.Repositories;
using MicroserviceCourse.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioingExt();

var app = builder.Build();

//seed data, thread bloklanmadan çalıştırılacak
app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data has been saved succesfully");
});

//minimal api's
app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();