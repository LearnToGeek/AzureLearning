using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChainingPattern
{
    public static class Function1
    {
        [FunctionName("StartOrchestration")]
        public static async Task<bool> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {

            order order = context.GetInput<order>();
            //Place order function call
            order savedOrder = await context.CallActivityAsync<order>("PlaceOrder", order);

            //Send queue message
            bool messsageSent = await context.CallActivityAsync<bool>("SendMessage", savedOrder);

            return messsageSent;
        }

        [FunctionName("PlaceOrder")]
        public static async Task<order> PlaceNewOrderAsync([ActivityTrigger] order obj, ILogger log)
        {
            ItemResponse<order> orderResult = null;
            try
            {

                string endpoint = "https://localhost:8081/";
                string masterKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

                CosmosClient client = new CosmosClient(endpoint, masterKey);

                Database database =  (await client.CreateDatabaseIfNotExistsAsync("Orders")).Database;

                Container container = (await database.CreateContainerIfNotExistsAsync("Orders", "/address/zip", 400)).Container;

                orderResult = await container.CreateItemAsync<order>(obj, new PartitionKey(obj.address.zip));
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
            }

            return orderResult;
        }

        [FunctionName("SendMessage")]
        public static string SendQueueMessage([ActivityTrigger] object obj, ILogger log)
        {
          //  log.LogInformation($"Saying hello to {obj.name}.");
            return $"Hello !";
        }

        [FunctionName("OchestrationInit")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post",Route ="orchestration/{functionName}")] HttpRequestMessage req,
            [DurableClient] IDurableClient starter,
            string functionName,
            ILogger log)
        {
            order postData = await req.Content.ReadAsAsync<order>();

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync(functionName, postData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }

    public class order
    {
        public string orderId { get; set; }
        public string customerId { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public address address { get; set; }
        public bool isGift { get; set; }
        public string coupon { get; set; }
    }

    public class address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string province { get; set; }
        public string country { get; set; }
    }
}