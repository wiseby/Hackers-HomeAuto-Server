using Application.Models;
using Application.MqttContextHandler;
using Application.Repositries;
using Autofac;
using DataAccess;
using Serilog;

namespace MqttServer.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(this ContainerBuilder builder, AppOptions configuration)
        {
            builder.Register<AppOptions>(
                c => configuration).AsSelf().SingleInstance();
            builder.Register(c => new MqttLogger("./logs/MqttServer.log")).As<ILogger>();
            builder.Register<MongoConnection>(
                c => new MongoConnection(configuration.ConnectionString))
                .As<IMongoConnection>()
                .SingleInstance();
            builder.RegisterType<MqttContextHandler>().As<IMqttContextHandler>();
            builder.RegisterType<MessageInterceptor>().AsSelf();
        }
    }
}