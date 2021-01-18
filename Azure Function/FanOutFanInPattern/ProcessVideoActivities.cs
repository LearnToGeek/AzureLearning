using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FanOutFanInPattern
{
    public static class ProcessVideoActivities
    {

        [FunctionName("A_TranscodeVideo")]
        public static async Task<VideoProcessInfo> TranscodeVideo([ActivityTrigger] VideoProcessInfo inputVideo, ILogger log)
        {
            log.LogInformation($"Transcode {inputVideo.Location} to {inputVideo.BitRate}");


            //simualate doing the activity
            await Task.Delay(10000);

            var TranscodeLocation = $"{Path.GetFileNameWithoutExtension(inputVideo.Location)} - " + $"{inputVideo.BitRate}kbps.mp4";

            return new VideoProcessInfo
            {
                Location = TranscodeLocation,
                BitRate = inputVideo.BitRate
            };
          
        }
    }
}