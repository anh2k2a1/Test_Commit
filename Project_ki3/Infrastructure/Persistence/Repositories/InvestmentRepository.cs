using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class InvestmentRepository : IInvestmentRepository
{
    private readonly IMongoCollection<Investment> _investments;

    public InvestmentRepository(MongoDbContext context)
    {
        _investments = context.Investments;
    }

    public async Task<Investment?> GetByIdAsync(string id)
    {
        return await _investments.Find(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Investment>> GetByUserIdAsync(string userId)
    {
        return await _investments.Find(i => i.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Investment investment)
    {
        await _investments.InsertOneAsync(investment);
    }

    public async Task<bool> UpdateAsync(Investment investment)
    {
        var result = await _investments.ReplaceOneAsync(i => i.Id == investment.Id, investment);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _investments.DeleteOneAsync(i => i.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}