using Azure.Core;
using Azure.Identity;
using BitBuggyRoleService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Identity.Client;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // We cannot use managed identities because they are not going to be in the same tenant. 
        // Azure B2C cannot have a subscription, and as a result, no managed identity.
        services.AddSingleton<IConfidentialClientApplication>(provider =>
        {
            string tenantId = Environment.GetEnvironmentVariable("TenantId") ?? throw new NullReferenceException("TenantId");
            string clientId = Environment.GetEnvironmentVariable("ClientId") ?? throw new NullReferenceException("ClientId");
            string clientSecret = Environment.GetEnvironmentVariable("ClientSecret") ?? throw new NullReferenceException("ClientSecret");

            return ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg, true)
                .Build();
        });

        services.AddTransient<ConfidentialClientCredentialProvider>();

        services.AddScoped<GraphServiceClient>(provider =>
        {
            ConfidentialClientCredentialProvider credentialProvider = provider.GetRequiredService<ConfidentialClientCredentialProvider>();
            return new GraphServiceClient(credentialProvider);
        });
    })
    .Build();

host.Run();
