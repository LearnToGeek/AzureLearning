using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SendNotificationSub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendNotificationController : ControllerBase
    {
        private readonly ILogger<SendNotificationController> _logger;

        public SendNotificationController(ILogger<SendNotificationController> logger)
        {
            _logger = logger;
        }

        private bool EventTypeSubscriptionValidation => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() == "SubscriptionValidation";

        private bool EventTypeNotification => HttpContext.Request.Headers["aeg-event-type"].FirstOrDefault() == "Notification";

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using (var requestStream = new StreamReader(Request.Body))
            {
                var bodyJson = await requestStream.ReadToEndAsync();

                var events = JsonConvert.DeserializeObject<List<EventGridEvent>>(bodyJson);

                if(EventTypeSubscriptionValidation)
                {
                    var subValidationEventData = ((JObject)events.First().Data).ToObject<SubscriptionValidationEventData>();

                    return new OkObjectResult(
                        new SubscriptionValidationResponse(subValidationEventData.ValidationCode)
                    );
                }
                else if(EventTypeNotification)
                {
                    var notificationEvent = events.First();

                    _logger.LogInformation(notificationEvent.Subject);

                    return new OkResult();
                }
            }

            return new BadRequestResult();
        }
    }
}
