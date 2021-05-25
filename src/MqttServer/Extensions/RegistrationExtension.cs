using Microsoft.Extensions.Configuration;
using Serilog;
using Autofac;
using Application.MqttContextHandler;
using DataAccess;

namespace MqttServer.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(this ContainerBuilder builder, IConfiguration configuration)
        {
            var appConf = configuration.GetSection(
                    MqttOptions.Position).Get<MqttOptions>();
            builder.Register<MqttOptions>(
                c => configuration.GetSection(
                    MqttOptions.Position).Get<MqttOptions>())
                    .AsSelf().SingleInstance();
            builder.Register(c =>
            {
                return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            })
            .As<ILogger>()
            .SingleInstance();
            builder.Register<MongoConnection>(
                c => new MongoConnection(appConf.ConnectionString))
                .As<IMongoConnection>()
                .SingleInstance();
            builder.RegisterType<MqttContextHandler>().As<IMqttContextHandler>();
            builder.RegisterType<MessageInterceptor>().AsSelf();
        }
    }
}