using Domain.Models;

namespace Domain.Interfaces;

public interface IFinancialCourseRepository
{
    Task<FinancialCourse?> GetByIdAsync(string id);
    Task<IEnumerable<FinancialCourse>> GetAllAsync();
    Task CreateAsync(FinancialCourse course);
    Task<bool> UpdateAsync(FinancialCourse course);
    Task<bool> DeleteAsync(string id);
}
