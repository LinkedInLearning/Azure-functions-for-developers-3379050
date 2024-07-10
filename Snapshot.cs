using HtmlAgilityPack;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace WebsiteWatcher;

public class Snapshot(ILogger<Snapshot> logger)
{
    [Function(nameof(Snapshot))]
    public void Run(
                [SqlTrigger("dbo.Websites", "WebsiteWatcher")] IReadOnlyList<SqlChange<Website>> changes)
    {
        foreach (var change in changes)
        {
            logger.LogInformation($"{change.Operation}");
            logger.LogInformation($"Id: {change.Item.Id} Url: {change.Item.Url}");

            if (change.Operation != SqlChangeOperation.Insert)
            {
                continue;
            }

            HtmlWeb web = new ();
            HtmlDocument doc = web.Load(change.Item.Url);

            var divWithContent = doc.DocumentNode.SelectSingleNode(change.Item.XPathExpression);
            var content = divWithContent != null ? divWithContent.InnerText.Trim() : "No content";

            logger.LogInformation(content);

        }
    }
}
