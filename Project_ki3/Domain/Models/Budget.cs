using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.Enum;

namespace Domain.Models;

public class Budget
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string UserId { get; set; }

    [BsonElement("category")]
    [BsonRepresentation(BsonType.String)] // Store as string in MongoDB
    public required string Category { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)] // Store as string in MongoDB
    public required string Type { get; set; }

    [BsonElement("amount")]
    public decimal Amount { get; set; }

    [BsonElement("period")]
    public required string Period { get; set; } // "weekly", "monthly", "yearly"

    [BsonElement("startDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime EndDate { get; set; }

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? UpdatedAt { get; set; } // Track updates
}
