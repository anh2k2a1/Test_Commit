using Autofac;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence.Services;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace API
{
    public class AutofacModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public AutofacModule()
        {
        }

        public AutofacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register repositories
            builder.RegisterType<MongoUserRepository>().As<Domain.Interfaces.IUserRepository>().InstancePerLifetimeScope();

            // Register services
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();

            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();

            // Register JwtTokenGenerator with parameters from configuration
            builder.RegisterType<JwtTokenGenerator>()
                .As<IJwtTokenGenerator>()
                .WithParameter("key", _configuration["Jwt:Key"])
                .WithParameter("issuer", _configuration["Jwt:Issuer"])
                .WithParameter("audience", _configuration["Jwt:Audience"])
                .InstancePerLifetimeScope();

            // Register MediatR handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerLifetimeScope();
        }
    }
}