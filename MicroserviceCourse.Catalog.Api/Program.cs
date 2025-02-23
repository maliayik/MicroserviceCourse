
using MediatR;
using MicroserviceCourse.Catalog.Api.Features.Categories;
using MicroserviceCourse.Catalog.Api.Features.Categories.Create;
using MicroserviceCourse.Catalog.Api.Options;
using MicroserviceCourse.Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();


var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();