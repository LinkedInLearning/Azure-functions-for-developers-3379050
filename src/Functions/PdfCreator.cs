using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using WebsiteWatcher.Services;

namespace WebsiteWatcher.Functions;

public class PdfCreator(ILogger<PdfCreator> logger, PdfCreatorService pdfCreatorService)
{
    [Function(nameof(PdfCreator))]
    public async Task Run(
      [SqlTrigger("dbo.Websites", "WebsiteWatcher")] SqlChange<Website>[] changes)
    {
        foreach (var change in changes)
        {
            if (change.Operation == SqlChangeOperation.Insert)
            {
                var result = await pdfCreatorService.ConvertPageToPdfAsync(change.Item.Url);

                logger.LogInformation($"PDF stream length is: {result.Length}");

                var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                var blobClient = new BlobClient(connectionString, "pdfs", $"{change.Item.Id}.pdf");
                await blobClient.UploadAsync(result);
            }
        }
    }
}