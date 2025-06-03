using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<Transaction> _transactions;

    public TransactionRepository(MongoDbContext context)
    {
        _transactions = context.Transactions;
    }

    public async Task<Transaction?> GetByIdAsync(string id)
    {
        return await _transactions.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
    {
        return await _transactions.Find(t => t.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(Transaction transaction)
    {
        await _transactions.InsertOneAsync(transaction);
    }

    public async Task<bool> UpdateAsync(Transaction transaction)
    {
        var result = await _transactions.ReplaceOneAsync(t => t.Id == transaction.Id, transaction);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _transactions.DeleteOneAsync(t => t.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
    public async Task<IEnumerable<Transaction>> GetByUserIdAndNumberAsync(string userId, int number, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Transaction>.Filter.Eq(t => t.UserId, userId);
        var sort = Builders<Transaction>.Sort.Descending(t => t.CreatedAt);

        return await _transactions
            .Find(filter)
            .Sort(sort)
            .Limit(number)
            .ToListAsync(cancellationToken);
    }

}