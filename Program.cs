using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebsiteWatcher;
using WebsiteWatcher.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(app =>
    {
        app.UseWhen<SafeBrowsingMiddleware>(context =>
        {
            return context.FunctionDefinition.Name == "Register";
        });
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<PdfCreatorService>();
        services.AddSingleton<SafeBrowsingService>();
    })
    .Build();

host.Run();
