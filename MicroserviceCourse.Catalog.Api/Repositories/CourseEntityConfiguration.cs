using MicroserviceCourse.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceCourse.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //MongoDb'de collection tablo ismimize karşılık gelir
            //Document tablo kayıtlarımıza karşılık gelir
            //Field kolonlarımıza karşılık gelir
            builder.ToCollection("courses");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).HasElementName("name");
            builder.Property(x => x.Description).HasMaxLength(1000).HasElementName("description");
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.Picture).HasElementName("picture");
            builder.Ignore(x => x.Category);
            builder.HasKey(x => x.Id);


            //feature tablosu olmayacağı için kurs ile feature arasında one to one ilişki kurulacak
            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(x => x.Duration).HasElementName("duration");
                feature.Property(x => x.Rating).HasElementName("rating");
                feature.Property(x => x.EducatorFullName).HasElementName("educatorfullname").HasMaxLength(100);
            });
        }
    }
}
