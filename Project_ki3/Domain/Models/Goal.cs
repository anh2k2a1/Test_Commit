using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public enum GoalType
    {
        Target,
        Milestone
    }

    public class Goal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonElement("title")]
        public required string Title { get; set; }

        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public required GoalType Type { get; set; }

        [BsonElement("progress")]
        public decimal Progress { get; set; } = 0; // Default progress is 0

        [BsonElement("target")]
        public decimal Target { get; set; }

        [BsonElement("milestoneLabels")]
        public List<string> MilestoneLabels { get; set; } = new(); // Default to an empty list

        [BsonElement("deadline")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? Deadline { get; set; }

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; } = false; // Default to not completed

        [BsonElement("reminderAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? ReminderAt { get; set; } // New property for reminders

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
}
