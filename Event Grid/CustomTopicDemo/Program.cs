using System;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace CustomTopicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string endpoint = "https://eventgridpsdemo.canadacentral-1.eventgrid.azure.net/api/events";
            string key = "r/38VQZ1Zuujz8UvI5TUn9KoMdZzrx74lUDxke7vBWY=";
            string topicHostName = new Uri(endpoint).Host;

            for (int i = 0; i < 10; i++)
            {
                EventGridEvent gridEvent = new EventGridEvent(
                  id: Guid.NewGuid().ToString(),
                  subject: "New Account - " + i.ToString(),
                  data: new { Message = "Hi, All geeky!" },
                  eventType: "NewAccountCreated",
                  eventTime: DateTime.Now,
                  dataVersion: "1.0"
                );

                TopicCredentials topicCredentials = new TopicCredentials(key);

                EventGridClient gridClient = new EventGridClient(topicCredentials);

                gridClient.PublishEventsAsync(topicHostName, new EventGridEvent[] { gridEvent });

                Console.WriteLine("Messaged published.");
            }
        }
    }
}
