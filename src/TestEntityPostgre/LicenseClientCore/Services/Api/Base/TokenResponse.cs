using Newtonsoft.Json;

namespace LicenseManagerClient.Services.Api.Base;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public struct TokenResponse
{
    public string tokenType;
    public string accessToken;
    public int expiresIn;
    public string refreshToken;
}