using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceCourse.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        //ihtiyaç olmadıkça Id değeri guid olmalı, ve budeğeri biz üretmeliyiz, guid üretme db ye bırakılmamalı! //biz bunun için snowflake algoritmasını kullanacağız
        [BsonElement("_id")]
        public Guid Id { get; set; } 


    }
}
