using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Application.Models;
using Application.MqttContextHandler;
using MQTTnet.Server;
using Serilog;

namespace MqttServer
{
    public class MessageInterceptor
    {
        private readonly ILogger logger;
        private readonly IMqttContextHandler handler;

        public MessageInterceptor(ILogger logger, IMqttContextHandler handler)
        {
            this.logger = logger;
            this.handler = handler;
        }


        public void Intercept(MqttApplicationMessageInterceptorContext context)
        {
            if (context.ApplicationMessage.Topic == "hha-server")
            {
                SaveMessage(context);
            }
        }

        private async void SaveMessage(MqttApplicationMessageInterceptorContext interceptContext)
        {
            try
            {
                var context = new Context()
                {
                    ClientId = interceptContext.ClientId,
                    Topic = interceptContext.ApplicationMessage.Topic,
                    Payload = ConvertPayload(interceptContext.ApplicationMessage.Payload)
                };

                await handler.SaveContext(context);
            }
            catch (JsonException e)
            {
                this.logger.Error("Failed to convert messagepayload. Saving message aborted", e);
            }
        }

        private Dictionary<string, object> ConvertPayload(byte[] messagePayload)
        {
            var jsonString = Encoding.UTF8.GetString(messagePayload);
            var deserializedPayload = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            var payload = new Dictionary<string, object>();
            foreach (var kvp in deserializedPayload)
            {
                payload.Add(kvp.Key, (object)kvp.Value);
            }
            return payload;
        }
    }
}