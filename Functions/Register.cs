using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebsiteWatcher.Services;

namespace WebsiteWatcher.Functions;

public class Register(ILogger<Register> logger, SafeBrowsingService safeBrowsingService)
{
    [Function(nameof(Register))]
    [SqlOutput("dbo.Websites", "WebsiteWatcher")]
    public async Task<Website> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        Website newWebsite = JsonSerializer.Deserialize<Website>(requestBody, options);

        newWebsite.Id = Guid.NewGuid();

        var result = safeBrowsingService.Check(newWebsite.Url);

        if (result.HasThreat)
        {
            var threats = string.Join(" ", result.Threats);
            logger.LogError($"Url has the following threats: {threats}");
            return null;
        }

        return newWebsite;
    }
}

public class Website
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string? XPathExpression { get; set; }
}