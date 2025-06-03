
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _notifications;

    public NotificationRepository(MongoDbContext context)
    {
        _notifications = context.Notifications;
    }

    public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
    {
        return await _notifications.Find(n => n.UserId == userId).ToListAsync();
    }

    public async Task<Notification> GetByIdAsync(string id)
    {
        return await _notifications.Find(n => n.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Notification> CreateAsync(Notification notification)
    {
        await _notifications.InsertOneAsync(notification);
        return notification;
    }

    public async Task<bool> UpdateAsync(Notification notification)
    {
        var result = await _notifications.ReplaceOneAsync(n => n.Id == notification.Id, notification);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _notifications.DeleteOneAsync(n => n.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<bool> MarkAsReadAsync(string id)
    {
        var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
        var result = await _notifications.UpdateOneAsync(n => n.Id == id, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}