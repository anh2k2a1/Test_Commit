using Domain.Models;

namespace Domain.Interfaces;

public interface IInvestmentRepository
{
    Task<Investment?> GetByIdAsync(string id);
    Task<IEnumerable<Investment>> GetByUserIdAsync(string userId);
    Task CreateAsync(Investment investment);
    Task<bool> UpdateAsync(Investment investment);
    Task<bool> DeleteAsync(string id);
}