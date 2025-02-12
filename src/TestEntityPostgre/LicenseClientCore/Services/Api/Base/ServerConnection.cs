using System.Net;
using System.Text;
using Library.Enums;
using LicenseManagerClient.Extensions;
using Newtonsoft.Json;

namespace LicenseManagerClient.Services.Api.Base;

public class ServerConnection
{
    private const string TOKEN_API = "login";

    private readonly ServerHttpClient _httpClient;
    private readonly string           _label;

    public string Token => _tokenResponse.HasValue ? _tokenResponse.Value.accessToken : String.Empty;

    private TokenResponse? _tokenResponse;


    public ServerConnection(ServerHttpClient client, string label)
    {
        _httpClient = client;
        _label      = label;
    }

    public async Task<(EResult Result, ServerCodeResponse Response)> Connect()
    {
        _httpClient.Initialize();

        //Some register/login method 

        return (EResult.Success, new ServerCodeResponse() { code = 0 });
    }

    public async Task<(EResult Result, ServerCodeResponse Response)> Disconnect()
    {
        _httpClient.Dispose();

        //Some logout method 

        return (EResult.Success, new ServerCodeResponse() { code = 0 });
    }

    public async Task<(EResult Result, ServerCodeResponse Response, TokenResponse ResultData)> Login(string username, string password, CancellationToken token = default)
    {
        var authObj = new { email = username, password = password };

        var (result, response, resultData) = await Provide(() => _httpClient.Post<TokenResponse>(TOKEN_API, authObj, token, null));

        if (result == EResult.Success)
            _tokenResponse = resultData;

        return (result, response, resultData);
    }

    public async Task<(EResult Result, ServerCodeResponse Response, T ResultData)> ProvideGet<T>(string api, CancellationToken token = default) =>
        await Provide(() => _httpClient.Get<T>(api, token, ConstructJwtAuthHeader()));

    public async Task<(EResult Result, ServerCodeResponse Response, T ResultData)> ProvidePost<T>(string api, object payload, CancellationToken token = default) =>
        await Provide(() => _httpClient.Post<T>(api, payload, token, ConstructJwtAuthHeader()));


    private async Task<(EResult Result, ServerCodeResponse Response, T ResultData)> Provide<T>(Func<Task<(EResult Result, HttpResponseMessage Response, T ResultData)>> request)
    {
        var requestResult = await request();

        if (requestResult.Result == EResult.Success)
            return (requestResult.Result, await requestResult.Response.ToServerCodeResponse(), requestResult.ResultData);

        if (requestResult.Result == EResult.Abort)
            return (EResult.Abort, new ServerCodeResponse(){message = await requestResult.Response.Content.ReadAsStringAsync()}, default);

        if (requestResult.Response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //Handle
        }

        try
        {
            return (requestResult.Result, JsonConvert.DeserializeObject<ServerCodeResponse>(await requestResult.Response.Content.ReadAsStringAsync()), requestResult.ResultData);
        }
        catch (JsonReaderException ex)
        {
            return (requestResult.Result, new ServerCodeResponse()
            {
                code    = (int)requestResult.Response.StatusCode,
                message = await requestResult.Response.Content.ReadAsStringAsync()
            }, requestResult.ResultData);
        }
    }

    private IEnumerable<ValueTuple<string, string>> ConstructJwtAuthHeader() =>
        new[] { ("Authorization", $"Bearer {Token}") };

    private IEnumerable<ValueTuple<string, string>> ConstructBasicAuthHeader(string name, string password) =>
        new[] { ("Authorization", $"Basic {Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{name}:{password}"))}") };
}