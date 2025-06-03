
using Domain.Models;

namespace Domain.Interfaces;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
    Task<Notification> GetByIdAsync(string id);
    Task<Notification> CreateAsync(Notification notification);
    Task<bool> UpdateAsync(Notification notification);
    Task<bool> DeleteAsync(string id);
    Task<bool> MarkAsReadAsync(string id);
}