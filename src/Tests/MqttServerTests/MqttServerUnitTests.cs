using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Application.Models;
using Application.MqttContextHandler;
using FakeItEasy;
using MQTTnet;
using MQTTnet.Diagnostics;
using MQTTnet.Server;
using MqttServer;
using Serilog;
using Xunit;

namespace Tests
{
    public class MqttServerUnitTests
    {
        private readonly string defaultRawPayload = "[{ \"temperature:\" \"21.3\" }, { \"humidity:\" \"75\" }]";
        private readonly Dictionary<string, object> defaultContextPayload = new Dictionary<string, object>() {
                    { "temperature", "21.3" },
                    { "humidity", "75" },
                };


        [Fact]
        public void Server_should_intercept_and_save_topics_with_any_clientId()
        {
            // Arrange
            var expectedContext = new Context()
            {
                ClientId = "123",
                Topic = "hha-server",
                Payload = defaultContextPayload
            };

            var rawPayload = defaultRawPayload;
            var payload = new Dictionary<string, string>() {
                { "clientId", expectedContext.ClientId },
                { "values", rawPayload }
             };
            var context = CreateMqttContext(expectedContext.ClientId, expectedContext.Topic, expectedContext.Payload);
            var logFake = A.Fake<ILogger>();
            var handlerFake = A.Fake<IMqttContextHandler>();
            var interceptor = new MessageInterceptor(logFake, handlerFake);

            // Act
            interceptor.Intercept(context);

            // Assert
            A.CallTo(() => handlerFake.SaveContext(
                A<Context>.That.Matches((context) => DoesContextMatch(context, expectedContext))))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Message_with_topic_hha_server_should_be_intercepted()
        {
            // Arrange
            var expectedContext = new Context()
            {
                ClientId = "123",
                Topic = "hha-server",
                Payload = defaultContextPayload
            };

            var rawPayload = defaultRawPayload;
            var payload = new Dictionary<string, string>() {
                { "clientId", expectedContext.ClientId },
                { "values", rawPayload }
             };
            var context = CreateMqttContext(expectedContext.ClientId, expectedContext.Topic, expectedContext.Payload);
            var logFake = A.Fake<ILogger>();
            var handlerFake = A.Fake<IMqttContextHandler>();
            var interceptor = new MessageInterceptor(logFake, handlerFake);

            // Act
            interceptor.Intercept(context);

            // Assert
            A.CallTo(() => handlerFake.SaveContext(
                A<Context>.That.Matches((context) => DoesContextMatch(context, expectedContext))))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Message_with_topic_other_then_hha_server_should_not_be_intercepted()
        {
            // Arrange
            var expectedContext = new Context()
            {
                ClientId = "123",
                Topic = "other-random-topic",
                Payload = defaultContextPayload
            };

            var rawPayload = defaultRawPayload;
            var payload = new Dictionary<string, string>() {
                { "clientId", expectedContext.ClientId },
                { "values", rawPayload }
             };

            var context = CreateMqttContext(expectedContext.ClientId, expectedContext.Topic, expectedContext.Payload);
            var logFake = A.Fake<ILogger>();
            var handlerFake = A.Fake<IMqttContextHandler>();
            var interceptor = new MessageInterceptor(logFake, handlerFake);

            // Act
            interceptor.Intercept(context);

            // Assert
            A.CallTo(() => handlerFake.SaveContext(
                A<Context>.That.Matches((context) => DoesContextMatch(context, expectedContext))))
                .MustNotHaveHappened();
        }

        private MqttApplicationMessageInterceptorContext CreateMqttContext(string clientId, string topic, Dictionary<string, object> payload)
        {
            var logger = A.Fake<IMqttNetScopedLogger>();
            var context = new MqttApplicationMessageInterceptorContext(clientId, null, logger);
            var payloadAsBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));
            var applicationMessage = new MqttApplicationMessage()
            {
                ContentType = "",
                CorrelationData = new byte[0],
                Dup = false,
                MessageExpiryInterval = 0,
                Payload = payloadAsBytes,
                PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.Unspecified,
                QualityOfServiceLevel = 0,
                ResponseTopic = "",
                Retain = false,
                SubscriptionIdentifiers = null,
                Topic = topic,
                TopicAlias = null,
                UserProperties = null
            };

            context.ApplicationMessage = applicationMessage;

            return context;
        }

        private bool DoesContextMatch(Context context, Context expectedContext)
        {
            if (context.ClientId == expectedContext.ClientId &&
                context.Topic == expectedContext.Topic &&
                PayloadComparer(context.Payload, expectedContext.Payload))
            {
                return true;
            }

            return false;
        }

        private bool PayloadComparer(Dictionary<string, object> x, Dictionary<string, object> y)
        {
            var matching = true;
            foreach (var kvp in x)
            {
                if (y.TryGetValue(kvp.Key, out object value))
                {
                    if (kvp.Value.ToString() != value.ToString())
                    {
                        matching = false;
                    }
                }
            }
            return matching;
        }
    }
}
