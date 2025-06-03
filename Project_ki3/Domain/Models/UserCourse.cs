using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class UserCourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonElement("courseId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CourseId { get; set; }

        [BsonElement("purchaseDate")]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [BsonElement("pricePaid")]
        public decimal PricePaid { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("completedItems")]
        public List<int> CompletedItems { get; set; } = new();
    }
}