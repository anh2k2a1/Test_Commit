using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class FinancialCourseItem
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("title")]
        public required string Title { get; set; }

        [BsonElement("subTitle")]
        public required string SubTitle { get; set; }

        [BsonElement("videoUrl")]
        public required string VideoUrl { get; set; }

        [BsonElement("isLearning")]
        public bool IsLearning { get; set; } = false; // Default to not learning
    }

    public class FinancialCourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public required string Title { get; set; }

        [BsonElement("desc")]
        public required string Description { get; set; }

        [BsonElement("rating")]
        public decimal Rating { get; set; } = 0; // Default rating is 0

        [BsonElement("reviewCount")]
        public int ReviewCount { get; set; } = 0; // Default review count is 0

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("discount")]
        public decimal Discount { get; set; } = 0; // Default discount is 0

        [BsonElement("imageUrl")]
        public required string ImageUrl { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new(); // Default to an empty list

        [BsonElement("courseList")]
        public List<FinancialCourseItem> CourseList { get; set; } = new(); // Default to an empty list

        [BsonElement("lock")]
        public bool Lock { get; set; } = false; // Default to unlocked
    }
}
