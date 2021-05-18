using System;
using Application.Extensions;
using Application.MqttContextHandler;
using Autofac;
using DataAccess;
using MQTTnet;
using MQTTnet.Server;

using Serilog;

namespace MqttServer
{
    public static class BrokerHost
    {
        private static MessageInterceptor interceptor;
        private static IMqttServer mqttServer;
        public static IContainer Container;

        public static async void InitializeAndRun(string[] args)
        {
            InitializeContainer();
            interceptor = Container.Resolve<MessageInterceptor>();
            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(1883)
                .WithConnectionBacklog(100)
                .WithApplicationMessageInterceptor(context =>
                {
                    interceptor.Intercept(context);
                })
                .WithDefaultEndpoint()
                .Build();

            mqttServer = new MqttFactory().CreateMqttServer();

            Log.Information("MqttServer starting...");
            await mqttServer.StartAsync(options);
        }

        public static void InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new MqttLogger("./logs/MqttServer.log")).As<ILogger>();
            builder.RegisterType<MqttContextHandler>().As<IMqttContextHandler>();
            builder.RegisterType<MessageInterceptor>().AsSelf();

            // Get configuration snapshot

            builder.Register<MongoConnection>(c => new MongoConnection("mongodb://root:example@localhost:27017"))
                .AsSelf()
                .SingleInstance();

            DependencyRegistration.Register(builder);

            Container = builder.Build();
        }

        public static int Stop()
        {
            Log.Information("Shutting down...");
            try
            {
                mqttServer.StopAsync().Wait();
                return 0;
            }
            catch (Exception e)
            {
                Log.Error("Unhandled Exception accured.");
                Log.Error(e.Message);
                return 1;
            }
        }
    }
}