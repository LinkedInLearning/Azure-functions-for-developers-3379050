using Azure.Storage.Blobs;
using HtmlAgilityPack;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using WebsiteWatcher.Services;

namespace WebsiteWatcher.Functions;

public class Watcher(ILogger<Watcher> logger, PdfCreatorService pdfCreatorService)
{
    private const string SqlInputQuery = @"SELECT w.Id, w.Url, w.XPathExpression, s.Content AS LatestContent 
                                        FROM dbo.Websites w 
                                        LEFT JOIN dbo.Snapshots s ON w.Id = s.Id 
                                        WHERE s.Timestamp = (SELECT MAX(Timestamp) FROM dbo.Snapshots WHERE Id = w.Id)";

    [Function(nameof(Watcher))]
    [SqlOutput("dbo.Snapshots", "WebsiteWatcher")]
    public async Task<SnapshotRecord?> Run([TimerTrigger("*/20 * * * * *")] TimerInfo myTimer,
        [SqlInput(SqlInputQuery, "WebsiteWatcher")] IReadOnlyList<WebsiteModel> websites)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        SnapshotRecord? result = null;

        foreach (var website in websites)
        {
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(website.Url);

            var divWithContent = doc.DocumentNode.SelectSingleNode(website.XPathExpression);
            var content = divWithContent != null ? divWithContent.InnerText.Trim() : "No content";
            content = content.Replace("Microsoft Entra", "Azure AD");
            var contentHasChanged = content != website.LatestContent;

            if (contentHasChanged)
            {
                logger.LogInformation("Content changed!");
                var newPdf = await pdfCreatorService.ConvertPageToPdfAsync(website.Url);
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:WebsiteWatcherStorage");
                var blobClient = new BlobClient(connectionString, "pdfs", $"{website.Id}-{DateTime.UtcNow:MMddyyyyhhmmss}.pdf");
                await blobClient.UploadAsync(newPdf);
                logger.LogInformation("New PDF uploaded.");
                result = new SnapshotRecord(website.Id, content);
            }
        }

        return result;
    }


}

public class WebsiteModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string? XPathExpression { get; set; }
    public string LatestContent { get; set; }
}