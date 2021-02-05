// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace eventgridApp.processevent
{
    public static class eventgridApp
    {
        [FunctionName("eventgridApp")]
        public static async Task ProcessBlob([EventGridTrigger]EventGridEvent eventGridEvent,
        [Blob("{data.url}",FileAccess.Read)] Stream inputBlobStream,
        ILogger log)
        {
            log.LogInformation($"Subject: {eventGridEvent.Subject}");

            var createBlobInfo = ((JObject)eventGridEvent.Data).ToObject<StorageBlobCreatedEventData>();
        }
    }
}
