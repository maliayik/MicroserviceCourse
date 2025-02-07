namespace MicroserviceCourse.Catalog.Api.Features.Courses
{
    //NOSQL veritabanı kullandığımız için feature için ıd tanımlaması yapmıcaz, course ıd si ile ilişkilendirilecek
    public class Feature
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
