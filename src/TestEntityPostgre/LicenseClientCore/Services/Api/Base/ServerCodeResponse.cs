using Newtonsoft.Json;

namespace LicenseManagerClient.Services.Api.Base;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public struct ServerCodeResponse
{
    public int    code;
    public string message;
}