using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBrokeredMessaging.Receiver
{
    class ReceiverConsole
    {
        static string connectionString = "Endpoint=sb://geekybus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tVO7Ny879JLGL5sL4KdOGTvJ6pKyAPKN9ORj+LIH2pw=";
        static string queuePath = "demoqueue";



        static void Main(string[] args)
        {
            //Create a queue client
            QueueClient queueClient = new QueueClient(connectionString, queuePath);

            //Create a message handler to receive messages
            queueClient.RegisterMessageHandler(ProcessMessageAsync, HandleExceptionAsync);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            //close a queue client
            queueClient.CloseAsync().Wait();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            string messageContent = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"Received - {messageContent}");
        }

        private static async Task HandleExceptionAsync(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        
    }
}
