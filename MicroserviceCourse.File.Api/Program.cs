using MicroserviceCourse.File.Api;
using MicroserviceCourse.File.Api.Features.File;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioingExt();

var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());

// wwwroot klasörünü dış dünyaya açmak için.
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();