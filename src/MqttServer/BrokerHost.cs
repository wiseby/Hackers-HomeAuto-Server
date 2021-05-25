using System;
using System.IO;
using MqttServer.Extensions;
using Application.Models;
using Autofac;
using Microsoft.Extensions.Configuration;
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
        private static IConfiguration configuration;

        public static async void InitializeAndRun(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
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
            // Get configuration snapshot
            var config = configuration.GetSection(AppOptions.Position).Get<AppOptions>();
            DependencyRegistration.Register(builder, config);
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