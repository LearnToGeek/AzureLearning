using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FanOutFanInPattern
{
    public static class ProcessVideoOrchestration
    {
        [FunctionName("O_EntryPoint")]
        public static async Task<string> O_EntryPoint(
            [OrchestrationTrigger] IDurableOrchestrationContext context,TraceWriter traceWriter)
        {
            var videoLocation = context.GetInput<string>();

            if (!context.IsReplaying)
                traceWriter.Info("About to call trancode video processing activity!");

            string transcodeLocation = null;
            string transcodeThumbnail = null;
            string withIntroLocation = null;

            try
            {
                var bitRates = new[] { 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000, 15000, 16000, 17000, 18000, 19000, 20000 };

                var transcodeTasks = new List<Task<VideoProcessInfo>>();

                foreach(int rate in bitRates)
                {
                    var info = new VideoProcessInfo { Location = videoLocation, BitRate = rate };
                    var task = context.CallActivityAsync<VideoProcessInfo>("A_TranscodeVideo", info);
                    transcodeTasks.Add(task);
                }

                var transcodeResult = await Task.WhenAll(transcodeTasks);
                transcodeLocation = transcodeResult[2].Location;
               

            }
            catch (System.Exception)
            {

                throw;
            }

            return transcodeLocation;
        }

        

        [FunctionName("OrchestrationInit")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("O_EntryPoint", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}