using Domain.Models;

namespace Domain.Interfaces;

public interface IGoalRepository
{
    Task<Goal?> GetByIdAsync(string id);
    Task<IEnumerable<Goal>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Goal>> GetGoalsWithUpcomingRemindersAsync(DateTime currentTime); // New method
    Task CreateAsync(Goal goal);
    Task<bool> UpdateAsync(Goal goal);
    Task<bool> DeleteAsync(string id);
}