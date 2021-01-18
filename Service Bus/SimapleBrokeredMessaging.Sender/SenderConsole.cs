using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimapleBrokeredMessaging.Sender
{
    class SenderConsole
    {
        static string connectionString = "Endpoint=sb://geekybus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tVO7Ny879JLGL5sL4KdOGTvJ6pKyAPKN9ORj+LIH2pw=";
        static string queuePath = "demoqueue";
        static void Main(string[] args)
        {
            //Create a queue client
            QueueClient queueClient = new QueueClient(connectionString, queuePath);

            //Send some messages
            for(int i=0; i<10; i++)
            {
                string messageContent = $"Message : {i}";

                Message message = new Message(Encoding.UTF8.GetBytes(messageContent));
                queueClient.SendAsync(message).Wait();

                Console.WriteLine("Sent : " + i);
            }

            //Close the client
            queueClient.CloseAsync().Wait();


            Console.WriteLine("Sent some message...");
            Console.ReadLine();
        }
    }
}
