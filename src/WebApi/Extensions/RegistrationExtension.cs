using Microsoft.Extensions.Configuration;
using Autofac;
using Application.Models;
using DataAccess;

namespace WebApi.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(
            this ContainerBuilder builder, IConfiguration configuration)
        {
            var appOptions = configuration
                .GetSection(AppOptions.Position).Get<AppOptions>();
            builder.Register<AppOptions>(
                c => appOptions).AsSelf().SingleInstance();
            builder.Register<MongoConnection>(
                c => new MongoConnection(appOptions.ConnectionString))
                .As<IMongoConnection>()
                .SingleInstance();

            Application.Extensions.DependencyRegistration.Register(builder);
        }
    }
}