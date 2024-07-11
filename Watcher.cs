using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WebsiteWatcher;

public class Watcher(ILogger<Watcher> logger)
{

    [Function(nameof(Watcher))]
    public void Run([TimerTrigger("/20 * * * * *")] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    }
}
