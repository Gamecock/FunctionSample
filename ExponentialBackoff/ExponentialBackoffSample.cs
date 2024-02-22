using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ExponentialBackoff
{
    public class ExponentialBackoffSample
    {
        private readonly ILogger _logger;

        public ExponentialBackoffSample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExponentialBackoffSample>();
        }

        [Function("ExponentialBackoffSample")]
        [ExponentialBackoffRetry(5,"00:02:00", "1:00:00")]
        public void Run([TimerTrigger("0 40 */2 * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            throw new Exception("Testing Backoff");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
