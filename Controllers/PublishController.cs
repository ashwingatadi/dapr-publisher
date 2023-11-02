using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly DaprClient daprClient;
        private readonly ILogger<PublishController> logger;

        public PublishController(DaprClient daprClient, ILogger<PublishController> logger) 
        { 
            this.daprClient = daprClient;
            this.logger = logger;
        }

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            await daprClient.PublishEventAsync("myapp-pubsub", "test-dapr-topic", $"{DateTime.Now} - {value}");
            logger.LogInformation($"Published data at {DateTime.Now} - {value}");
        }
    }
}
