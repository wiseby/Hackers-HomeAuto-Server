using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Autofac;
using MQTTnet;
using MQTTnet.Server;
using MqttServer.Extensions;

using Serilog;

namespace MqttServer
{
    public static class BrokerHost
    {
        private static MessageInterceptor interceptor;
        private static IMqttServer mqttServer;
        public static IContainer Container;
        private static IConfiguration configuration;
        public static MqttOptions options;

        public static async void InitializeAndRun(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();

            InitializeContainer();
            Log.Logger = Container.Resolve<ILogger>();
            interceptor = Container.Resolve<MessageInterceptor>();
            options = Container.Resolve<MqttOptions>();
            var serverOptions = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(options.BrokerPort)
                .WithConnectionBacklog(options.ConnectionBacklog)
                .WithApplicationMessageInterceptor(context =>
                {
                    interceptor.Intercept(context);
                })
                .WithDefaultEndpoint()
                .Build();

            mqttServer = new MqttFactory().CreateMqttServer();

            Log.Information("MqttServer starting...");
            await mqttServer.StartAsync(serverOptions);
        }

        public static void InitializeContainer()
        {
            var builder = new ContainerBuilder();
            DependencyRegistration.Register(builder, configuration);
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