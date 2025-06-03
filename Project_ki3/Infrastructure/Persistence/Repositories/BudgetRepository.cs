using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IMongoCollection<Budget> _budgets;

    public BudgetRepository(MongoDbContext context)
    {
        _budgets = context.Budgets;
    }

    public async Task<Budget?> GetByIdAsync(string id)
    {
        return await _budgets.Find(b => b.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Budget>> GetByUserIdAsync(string userId)
    {
        return await _budgets.Find(b => b.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Budget budget)
    {
        await _budgets.InsertOneAsync(budget);
    }

    public async Task<bool> UpdateAsync(Budget budget)
    {
        var result = await _budgets.ReplaceOneAsync(b => b.Id == budget.Id, budget);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _budgets.DeleteOneAsync(b => b.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<ReportBudgetResult> GetReportByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var currentMonthStart = new DateTime(now.Year, now.Month, 1);
        var nextMonthStart = currentMonthStart.AddMonths(1);
        var previousMonthStart = currentMonthStart.AddMonths(-1);

        var filter = Builders<Budget>.Filter.And(
            Builders<Budget>.Filter.Eq(b => b.UserId, userId),
            Builders<Budget>.Filter.Or(
                Builders<Budget>.Filter.And(
                    Builders<Budget>.Filter.Lte(b => b.StartDate, nextMonthStart.AddDays(-1)),
                    Builders<Budget>.Filter.Gte(b => b.EndDate, previousMonthStart)
                )
            )
        );

        var budgets = await _budgets.Find(filter).ToListAsync(cancellationToken);

        Dictionary<string, decimal> GroupByType(DateTime monthStart, DateTime monthEnd) =>
            budgets
                .Where(b => b.StartDate < monthEnd && b.EndDate >= monthStart)
                .GroupBy(b => b.Type.ToLowerInvariant())
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(b => b.Amount)
                );

        return new ReportBudgetResult
        {
            CurrentMonth = GroupByType(currentMonthStart, nextMonthStart),
            PreviousMonth = GroupByType(previousMonthStart, currentMonthStart)
        };
    }

}