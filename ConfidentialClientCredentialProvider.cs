using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuggyRoleService;

/// <summary>
/// Does a simple token acquisition.
/// Once the acquisition is complete, it adds it to the headers. 
/// </summary>
/// <param name="confidentialClient"></param>
internal class ConfidentialClientCredentialProvider(IConfidentialClientApplication confidentialClient) : IAuthenticationProvider
{

    private readonly IConfidentialClientApplication _confidentialClient = confidentialClient;
    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        AuthenticationResult result = await _confidentialClient
            .AcquireTokenForClient(["User.Read.All"])
            .ExecuteAsync(cancellationToken);

        request.Headers.Add("Authorization", $"Bearer {result.AccessToken}");
    }
}
