using Domain.Models;

namespace Domain.Interfaces;

public interface IDebtRepository
{
    Task<Debt?> GetByIdAsync(string id);
    Task<IEnumerable<Debt>> GetByUserIdAsync(string userId);
    Task CreateAsync(Debt debt);
    Task<bool> UpdateAsync(Debt debt);
    Task<bool> DeleteAsync(string id);
}