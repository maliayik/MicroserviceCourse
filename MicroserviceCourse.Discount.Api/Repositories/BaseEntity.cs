using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceCourse.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }

    }
}
