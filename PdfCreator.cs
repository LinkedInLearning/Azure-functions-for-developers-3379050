using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;

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
                var result = await ConvertPageToPdfAsync(change.Item.Url);

                logger.LogInformation($"PDF stream length is: {result.Length}");

                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:WebsiteWatcherStorage");
                var blobClient = new BlobClient(connectionString, "pdfs", $"{change.Item.Id}.pdf");
                await blobClient.UploadAsync(result);
            }
        }
    }

    private async Task<Stream> ConvertPageToPdfAsync(string url)
    {
        var browserFetcher = new BrowserFetcher();

        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        await page.EvaluateExpressionHandleAsync("document.fonts.ready");
        var result = await page.PdfStreamAsync();
        result.Position = 0;

        return result;
    }
}