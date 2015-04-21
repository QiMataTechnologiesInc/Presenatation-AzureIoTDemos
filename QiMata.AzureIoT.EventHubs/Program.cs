using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace QiMata.AzureIoT.EventHubs
{
    class Program
    {
        static string eventHubName = "qimatainternetofthingsdemo";
        //Recieve Rule
        static string connectionString = "Endpoint=sb://qimatainternetofthingsdemo-ns.servicebus.windows.net/;SharedAccessKeyName=SendRule;SharedAccessKey=FMYB6V9mMbdALXVRKyUTWxt+D8Srm5aDj+7CW/UBYCU=";

        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to stop the sender process");
            Console.WriteLine("Press Enter to start now");
            Console.ReadLine();
            SendingRandomMessages().Wait();
        }

        static async Task SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            while (true)
            {
                try
                {
                    var message = Guid.NewGuid().ToString();
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now.ToString(), message);
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now.ToString(), exception.Message);
                    Console.ResetColor();
                }

                await Task.Delay(200);
            }
        }
    }
}
