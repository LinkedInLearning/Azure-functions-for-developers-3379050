using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace WebsiteWatcher;

public class PdfCreator(ILogger<PdfCreator> logger)
{
    [Function(nameof(PdfCreator))]
    public async Task Run(
      [SqlTrigger("dbo.Websites", "WebsiteWatcher")] SqlChange<Website>[] changes)
    {
        foreach (var change in changes)
        {
            if (change.Operation == SqlChangeOperation.Insert)
            {
                
            }
        }
    }
}