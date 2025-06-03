using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class DebtRepository : IDebtRepository
{
    private readonly IMongoCollection<Debt> _debts;

    public DebtRepository(MongoDbContext context)
    {
        _debts = context.Debts;
    }

    public async Task<Debt?> GetByIdAsync(string id)
    {
        return await _debts.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Debt>> GetByUserIdAsync(string userId)
    {
        return await _debts.Find(d => d.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Debt debt)
    {
        await _debts.InsertOneAsync(debt);
    }

    public async Task<bool> UpdateAsync(Debt debt)
    {
        var result = await _debts.ReplaceOneAsync(d => d.Id == debt.Id, debt);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _debts.DeleteOneAsync(d => d.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}