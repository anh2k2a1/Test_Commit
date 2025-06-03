using Domain.Models;

namespace Domain.Interfaces;

public interface IUserCourseRepository
{
    Task<UserCourse?> GetByIdAsync(string id);
    Task<IEnumerable<UserCourse>> GetByUserIdAsync(string userId);
    Task<UserCourse?> GetByUserAndCourseIdAsync(string userId, string courseId);
    Task CreateAsync(UserCourse userCourse);
    Task<bool> UpdateAsync(UserCourse userCourse);
    Task<bool> DeleteAsync(string id);
}