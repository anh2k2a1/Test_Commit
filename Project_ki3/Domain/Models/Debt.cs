using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Debt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonElement("creditor")]
        public required string Creditor { get; set; }

        [BsonElement("type")]
        public required string Type { get; set; }

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("remainingAmount")]
        public decimal RemainingAmount { get; set; }

        [BsonElement("monthlyPayment")]
        public decimal MonthlyPayment { get; set; }

        [BsonElement("nextDueDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime NextDueDate { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
