using Application.Models;
using Application.Repositries;
using Autofac;
using DataAccess;

namespace Application.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(this ContainerBuilder builder, AppOptions configuration)
        {
            builder.Register<AppOptions>(
                c => configuration).AsSelf().SingleInstance();
            builder.Register<MongoConnection>(
                c => new MongoConnection(configuration.ConnectionString))
                .As<IMongoConnection>()
                .SingleInstance();

            builder.RegisterType<NodeRepository>().As<INodeRepository>();
            builder.RegisterType<ReadingRepository>().As<IReadingRepository>();
        }
    }
}