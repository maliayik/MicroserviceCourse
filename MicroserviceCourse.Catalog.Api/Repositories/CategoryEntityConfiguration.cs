using MicroserviceCourse.Catalog.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace MicroserviceCourse.Catalog.Api.Repositories
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).HasElementName("name");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(c => c.Courses);//category ve kurs tabloları içerisinde tekrar değerler oluşmasın diye ignore ediyoruz
        }
    }
}
