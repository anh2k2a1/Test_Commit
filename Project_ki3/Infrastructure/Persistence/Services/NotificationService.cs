
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Persistence.Service;

public class NotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
    {
        _notificationRepository = notificationRepository;
        _hubContext = hubContext;
    }

    public async Task SendReminderNotification(string userId, string message)
    {
        // Create and save notification
        var notification = new Notification
        {
            UserId = userId,
            Message = message,
            Type = "Reminder"
        };

        await _notificationRepository.CreateAsync(notification);

        // Send real-time notification via SignalR
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
    }

    public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
    {
        return await _notificationRepository.GetUserNotificationsAsync(userId);
    }

    public async Task<bool> MarkAsReadAsync(string notificationId)
    {
        return await _notificationRepository.MarkAsReadAsync(notificationId);
    }
}