using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBrokeredMessaging.ChatConsole
{
    class ChatApplication
    {

        static string connectionString = "Endpoint=sb://geekybus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tVO7Ny879JLGL5sL4KdOGTvJ6pKyAPKN9ORj+LIH2pw=";
        static string topicPath = "chattopic";

        static void Main(string[] args)
        {
            Console.Write("Enter name: ");
            string username = Console.ReadLine();

            // Create a management client to manage artifacts
            ManagementClient managementClient = new ManagementClient(connectionString);
            
            // Create a topic is it does not exist
            if(!managementClient.TopicExistsAsync(topicPath).Result)
            {
                managementClient.CreateTopicAsync(topicPath).Wait();
            }

            //Create a subscription for the user
            SubscriptionDescription subscriptionDescription = new SubscriptionDescription(topicPath, username)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            };

            managementClient.CreateSubscriptionAsync(subscriptionDescription).Wait();

            // Create clients
            TopicClient topicClient = new TopicClient(connectionString, topicPath);
            SubscriptionClient subscriptionClient = new SubscriptionClient(connectionString, topicPath, username);

            // Create a message pump for receiving messages 
            subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, ExceptionReceivedHandler);

            //Send a message to say you are here
            Message welcomeMessage = new Message(Encoding.UTF8.GetBytes($"Welcome to this room {username}!"));
            welcomeMessage.Label = username;
            topicClient.SendAsync(welcomeMessage).Wait();

            while (true)
            {
                string textMessage = Console.ReadLine();

                if (textMessage.Equals("exit")) break;

                //Send a chat message
                Message chatMessage = new Message(Encoding.UTF8.GetBytes(textMessage));
                chatMessage.Label = username;
                topicClient.SendAsync(chatMessage);
            }

            //Send a message to say you are leaving
            Message goodByeMessage = new Message(Encoding.UTF8.GetBytes("Good bye! Hope you had great conversation."));
            goodByeMessage.Label = username;
            topicClient.SendAsync(goodByeMessage).Wait();

            //Close all clients
            topicClient.CloseAsync().Wait();
            subscriptionClient.CloseAsync().Wait();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            //Deserialize the message body

            string deserializedMessage = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"{message.Label}> {deserializedMessage}");
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}
