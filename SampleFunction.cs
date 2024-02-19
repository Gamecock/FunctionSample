using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Azure
{
    public class SampleFunction
    {
        private readonly ILogger _logger;

        public SampleFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SampleFunction>();
        }

        [Function("SampleFunction")]
        [ExponentialBackoffRetry(5,"00:02:00", "1:00:00")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
