using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContext;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Service;
using Infrastructure.Services;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Domain.Models;
using MongoDB.Driver;
using Application.CQRS.Commands.Users;
using Application.CQRS.Handlers.Users; // <-- Ensure this is included
using Infrastructure.Persistence.Services;

namespace Infrastructure
{
    public static class ContainerConfig
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Legacy registration method - can be removed once Autofac is fully implemented
            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Remove MediatR registration from IServiceCollection to avoid duplicate registrations
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static ContainerBuilder AddGenericHandlers(this ContainerBuilder builder)
        {
            // Register MediatR with scoped lifetime
            var applicationAssembly = Assembly.GetAssembly(typeof(CreateUserCommand));
            var handlerAssembly = Assembly.GetAssembly(typeof(ForgotPasswordHandler));
            var mediatRConfiguration = MediatRConfigurationBuilder
                .Create(applicationAssembly, handlerAssembly) // Register both command and handler assemblies
                .WithRegistrationScope(RegistrationScope.Scoped)
                .Build();

            builder.RegisterMediatR(mediatRConfiguration);

            // Register repositories
            builder.RegisterType<MongoDbContext>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<UserRepository>()
                   .As<IUserRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<GoalRepository>()
                   .As<IGoalRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<InvestmentRepository>()
                   .As<IInvestmentRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TransactionRepository>()
                   .As<ITransactionRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DebtRepository>()
                   .As<IDebtRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<FinancialCourseRepository>()
                   .As<IFinancialCourseRepository>()
                   .InstancePerLifetimeScope();

            // Register services
            builder.RegisterType<GoalReminderService>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.Register(ctx => ctx.Resolve<MongoDbContext>().Users)
                   .As<IMongoCollection<User>>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<NotificationService>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterType<LoggingService>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            // Register all handler types in the handler assembly
            builder.RegisterAssemblyTypes(handlerAssembly)
                   .AsImplementedInterfaces();

            // Populate ASP.NET Core services
            var services = new ServiceCollection();
            builder.Populate(services);

            return builder;
        }
    }
}
