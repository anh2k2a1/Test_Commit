using Domain.Models;

namespace Domain.Interfaces;

public interface IBudgetRepository
{
    Task<Budget?> GetByIdAsync(string id);
    Task<IEnumerable<Budget>> GetByUserIdAsync(string userId);
    Task CreateAsync(Budget budget);
    Task<bool> UpdateAsync(Budget budget);
    Task<bool> DeleteAsync(string id);
    Task<ReportBudgetResult> GetReportByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}

