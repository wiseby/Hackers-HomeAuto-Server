namespace MqttServer
{
    public class MqttOptions
    {
        public const string Position = "MqttOptions";
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public int BrokerPort { get; set; }
        public int ConnectionBacklog { get; set; }
    }
}