using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly IMongoCollection<Goal> _goals;

    public GoalRepository(MongoDbContext context)
    {
        _goals = context.Goals;
    }

    public async Task<Goal?> GetByIdAsync(string id)
    {
        return await _goals.Find(g => g.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Goal>> GetByUserIdAsync(string userId)
    {
        return await _goals.Find(g => g.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Goal>> GetGoalsWithUpcomingRemindersAsync(DateTime currentTime)
    {
        return await _goals.Find(g => g.ReminderAt != null && g.ReminderAt <= currentTime).ToListAsync();
    }

    public async Task CreateAsync(Goal goal)
    {
        await _goals.InsertOneAsync(goal);
    }

    public async Task<bool> UpdateAsync(Goal goal)
    {
        var result = await _goals.ReplaceOneAsync(g => g.Id == goal.Id, goal);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _goals.DeleteOneAsync(g => g.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}