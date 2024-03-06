using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueTrigger
{
    public class QueueException
    {
        private readonly ILogger<QueueException> _logger;

        public QueueException(ILogger<QueueException> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueTrigger))]
        public void Run([QueueTrigger("myqueue-items", Connection = "QueueTest")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
