using System;

namespace MqttServer
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {   
            Console.WriteLine("MQTT Server is starting...");
            BrokerHost.InitializeAndRun(args);
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
            return BrokerHost.Stop();
        }
    }
}