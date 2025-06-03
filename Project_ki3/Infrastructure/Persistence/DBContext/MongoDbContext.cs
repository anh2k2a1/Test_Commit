using Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Persistence.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDb:ConnectionString"];
            var databaseName = configuration["MongoDb:DatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Transaction> Transactions => _database.GetCollection<Transaction>("transactions");
        public IMongoCollection<Budget> Budgets => _database.GetCollection<Budget>("budgets");
        public IMongoCollection<Goal> Goals => _database.GetCollection<Goal>("goals");
        public IMongoCollection<Investment> Investments => _database.GetCollection<Investment>("investments");
        public IMongoCollection<Debt> Debts => _database.GetCollection<Debt>("debts");
        // Add this property to your existing MongoDbContext class
        public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
        public IMongoCollection<FinancialCourse> FinancialCourses => _database.GetCollection<FinancialCourse>("financialCourses");
        public IMongoCollection<UserCourse> UserCourses => _database.GetCollection<UserCourse>("educationalResources");
        public IMongoCollection<FinancialCourse> FinancialCourse => _database.GetCollection<FinancialCourse>("educationalResources");
    }
}
