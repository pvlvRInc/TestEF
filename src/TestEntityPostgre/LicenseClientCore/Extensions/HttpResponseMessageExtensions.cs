using LicenseManagerClient.Services.Api.Base;

namespace LicenseManagerClient.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ServerCodeResponse> ToServerCodeResponse(this HttpResponseMessage httpResponseMessage) =>
        new()
        {
            code    = (int)httpResponseMessage.StatusCode,
            message = await httpResponseMessage.Content.ReadAsStringAsync()
        };
}