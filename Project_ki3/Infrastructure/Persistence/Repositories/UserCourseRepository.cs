using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Persistence.DbContext;

namespace Infrastructure.Persistence.Repositories
{
    public class UserCourseRepository(MongoDbContext context) : IUserCourseRepository
    {
        private readonly IMongoCollection<UserCourse> _userCourses = context.UserCourses;

        public async Task<UserCourse?> GetByIdAsync(string id)
        {
            return await _userCourses.Find(uc => uc.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetByUserIdAsync(string userId)
        {
            return await _userCourses.Find(uc => uc.UserId == userId).ToListAsync();
        }

        public async Task<UserCourse?> GetByUserAndCourseIdAsync(string userId, string courseId)
        {
            return await _userCourses.Find(uc => uc.UserId == userId && uc.CourseId == courseId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserCourse userCourse)
        {
            await _userCourses.InsertOneAsync(userCourse);
        }

        public async Task<bool> UpdateAsync(UserCourse userCourse)
        {
            var result = await _userCourses.ReplaceOneAsync(uc => uc.Id == userCourse.Id, userCourse);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _userCourses.DeleteOneAsync(uc => uc.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}