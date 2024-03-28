using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitBuggyRoleService.Models;

/// <summary>
/// Represents a request for claims modifications. 
/// Contains information about the user object. 
/// </summary>
internal class ClaimsRequestModel
{
    [JsonPropertyName("objectId")]
    Guid UserId { get; set; }
}
