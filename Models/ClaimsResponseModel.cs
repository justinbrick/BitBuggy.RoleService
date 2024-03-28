using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitBuggyRoleService.Models;

/// <summary>
/// Claims response, which will return the claims that we need to add to this token. 
/// </summary>
internal class ClaimsResponseModel
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = "1.0.0";

    [JsonPropertyName("action")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ClaimResponseAction Action { get; set; }

    [JsonPropertyName("userMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UserMessage { get; set; }

    /// <summary>
    /// A list of roles separated by spaces. 
    /// </summary>
    [JsonPropertyName("extension_roles")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RoleString { get; set; }
}

internal enum ClaimResponseAction
{
    Continue,
    ShowBlockPage
}
