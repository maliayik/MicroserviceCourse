using System.Reflection;
using MicroserviceCourse.Catalog.Api.Features.Categories;
using MicroserviceCourse.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceCourse.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure yaptığımız entitylerin çalıştığımız assembly içerisinde olduğunu belirtiyoruz
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
