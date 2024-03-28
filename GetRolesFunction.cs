using System.Net;
using System.Text.Json;
using BitBuggyRoleService.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace BitBuggyRoleService;

public class GetRolesFunction(ILogger<GetRolesFunction> logger) //, GraphServiceClient graph
{
    private readonly ILogger _logger = logger;
    //private readonly GraphServiceClient _graph = graph;

    [Function("GetRoles")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        ClaimsRequestModel? request = JsonSerializer.Deserialize<ClaimsRequestModel>(req.Body);
        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

        if (request is null)
        {
            ClaimsResponseModel err = new()
            {
                Action = ClaimResponseAction.ShowBlockPage,
                UserMessage = "Internal Error: Incorrect Claims Model"
            };

            await response.WriteAsJsonAsync(err);
            return response;
        }

        ClaimsResponseModel cont = new()
        {
            Action = ClaimResponseAction.Continue,
            RoleString = "test"
        };
        await response.WriteAsJsonAsync(cont);

        return response;
    }

    public bool IsValid(string user, string pass)
    {
        return true;
    }
}
