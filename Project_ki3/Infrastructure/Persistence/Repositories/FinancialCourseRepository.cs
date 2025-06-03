using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.DbContext;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class FinancialCourseRepository : IFinancialCourseRepository
{
    private readonly IMongoCollection<FinancialCourse> _courses;

    public FinancialCourseRepository(MongoDbContext context)
    {
        _courses = context.FinancialCourse;
    }

    public async Task<FinancialCourse?> GetByIdAsync(string id)
    {
        return await _courses.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FinancialCourse>> GetAllAsync()
    {
        return await _courses.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(FinancialCourse course)
    {
        await _courses.InsertOneAsync(course);
    }

    public async Task<bool> UpdateAsync(FinancialCourse course)
    {
        var result = await _courses.ReplaceOneAsync(c => c.Id == course.Id, course);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _courses.DeleteOneAsync(c => c.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}