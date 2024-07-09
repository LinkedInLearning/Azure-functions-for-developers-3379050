using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace WebsiteWatcher;

public class Register(ILogger<Register> logger)
{
    [Function(nameof(Register))]
    [SqlOutput("dbo.Websites", connectionStringSetting: "LocalSql")]
    public async Task<Website> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        Website newWebsite = JsonSerializer.Deserialize<Website>(requestBody, options);
        
        newWebsite.Id = Guid.NewGuid();

        return newWebsite;
    }
}

public class Website
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string? XPathExpression { get; set; }
}