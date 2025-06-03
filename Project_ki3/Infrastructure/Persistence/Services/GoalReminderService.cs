using Domain.Interfaces;
using Infrastructure.Persistence.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class GoalReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GoalReminderService> _logger;
    private const int ReminderCheckIntervalMinutes = 5; // Interval to check for reminders

    public GoalReminderService(IServiceProvider serviceProvider, ILogger<GoalReminderService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Goal Reminder Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var goalRepository = scope.ServiceProvider.GetRequiredService<IGoalRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                // Get goals with reminders due
                var now = DateTime.UtcNow;
                var goals = await goalRepository.GetGoalsWithUpcomingRemindersAsync(now);

                if (goals.Any())
                {
                    _logger.LogInformation("{Count} goals with upcoming reminders found.", goals.Count());

                    foreach (var goal in goals)
                    {
                        try
                        {
                            // Send reminder notification
                            var message = $"Reminder: Your goal '{goal.Title}' is due soon!";
                            await notificationService.SendReminderNotification(goal.UserId, message);
                            _logger.LogInformation("Reminder sent for goal '{GoalTitle}' to user '{UserId}'.", goal.Title, goal.UserId);

                            // Update the goal to avoid duplicate reminders
                            goal.ReminderAt = null; // Clear the reminder
                            await goalRepository.UpdateAsync(goal);
                            _logger.LogInformation("Reminder cleared for goal '{GoalTitle}'.", goal.Title);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to send reminder for goal '{GoalTitle}' to user '{UserId}'.", goal.Title, goal.UserId);
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("No goals with upcoming reminders found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing goal reminders.");
            }

            // Wait for a specified interval before checking again
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(ReminderCheckIntervalMinutes), stoppingToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Goal Reminder Service task was canceled.");
                break;
            }
        }

        _logger.LogInformation("Goal Reminder Service is stopping.");
    }
}

