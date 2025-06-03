using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationsController(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    // GET: api/notifications/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        return notification is null ? NotFound("Notification not found.") : Ok(notification);
    }

    // GET: api/notifications/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // Fix: Use GetUserNotificationsAsync instead of GetByIdAsync
        var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
        var paginatedNotifications = notifications
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return Ok(paginatedNotifications);
    }

    [HttpGet("user/{userId}/unread-count")]
    public async Task<IActionResult> GetUnreadCount(string userId)
    {
        // Fix: Use GetUserNotificationsAsync instead of GetByIdAsync
        var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
        var unreadCount = notifications.Count(n => !n.IsRead);
        return Ok(new { UnreadCount = unreadCount });
    }

    [HttpGet("user/{userId}/filter")]
    public async Task<IActionResult> GetByType(string userId, [FromQuery] string type)
    {
        // Fix: Use GetUserNotificationsAsync instead of GetByIdAsync
        var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
        var filteredNotifications = notifications.Where(n => n.Type == type);
        return Ok(filteredNotifications);
    }

    // POST: api/notifications/{id}/mark-as-read
    [HttpPost("{id}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead(string id)
    {
        var success = await _notificationRepository.MarkAsReadAsync(id);
        return success ? NoContent() : NotFound("Notification not found.");
    }

    // DELETE: api/notifications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _notificationRepository.DeleteAsync(id);
        return success ? NoContent() : NotFound("Notification not found.");
    }
}
