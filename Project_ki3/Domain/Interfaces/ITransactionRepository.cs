using Domain.Models;

namespace Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(string id);
    Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
    Task CreateAsync(Transaction transaction);
    Task<bool> UpdateAsync(Transaction transaction);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<Transaction>> GetByUserIdAndNumberAsync(string userId, int number, CancellationToken cancellationToken = default);

}