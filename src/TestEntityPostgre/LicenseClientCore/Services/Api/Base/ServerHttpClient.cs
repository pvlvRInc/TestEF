using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Library.Enums;
using LicenseManagerClient.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LicenseManagerClient.Services.Api.Base;

public class ServerHttpClient : IInitializable, IDisposable
{
    private readonly string _url;

    private readonly JsonSerializerSettings _serializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };

    private HttpClient _httpClient;

    public void Initialize()
    {
        //Bypass ssl certificate  
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        _httpClient = new HttpClient(clientHandler);
    }

    public void Dispose() =>
        _httpClient?.Dispose();

    public ServerHttpClient(string url)
    {
        _url = url;
    }


    public async Task<(EResult status, HttpResponseMessage response, T ResultData)> Post<T>(string api, object payload, CancellationToken token, IEnumerable<(string, string)> extraHeaders = null)
    {
        string serializeObject = JsonConvert.SerializeObject(payload);
        var data = new StringContent(serializeObject, Encoding.UTF8, "application/json"); //Handle serialization errors
        //Log POST
        Console.WriteLine($"[POST] [{_url}{api}] Data: {serializeObject}");
        // Console.WriteLine($"[POST] After serialization: {await data.ReadAsStringAsync()}");

        var (result, response) = await SendRequest(api, HttpMethod.Post, token, data, extraHeaders: extraHeaders);
        var resultData = default(T);

        if (result == EResult.Success)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                //Log Raw Content
                resultData = JsonConvert.DeserializeObject<T>(content, _serializerSettings);
            }
            catch (Exception ex)
            {
                //Log error 
                return (EResult.Abort, response, default);
            }
            //Log Content
        }
        else
        {
            //Log error
        }

        return (result, response, resultData);
    }

    public async Task<(EResult status, HttpResponseMessage response, T ResultData)> Get<T>(string api, CancellationToken token, IEnumerable<(string, string)> extraHeaders = null)
    {
        //Log GET
        var (result, response) = await SendRequest(api, HttpMethod.Get, token, extraHeaders: extraHeaders);
        var resultData = default(T);

        if (result == EResult.Success)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                //Log Raw Content
                resultData = JsonConvert.DeserializeObject<T>(content, _serializerSettings);
            }
            catch (Exception ex)
            {
                //Log error 
                return (EResult.Abort, response, default);
            }
            //Log Content
        }
        else
        {
            //Log error
        }

        return (result, response, resultData);
    }

    private async Task<(EResult status, HttpResponseMessage response)> SendRequest(string api, HttpMethod method, CancellationToken token, StringContent? content = null, IEnumerable<(string, string)> extraHeaders = null)
    {
        var watch = Stopwatch.StartNew();
        var request = new HttpRequestMessage(method, $"{_url}{api}");

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        // request.Headers.Add("Content-Type", MediaTypeNames.Application.Json);

        if (extraHeaders is not null)
        {
            foreach (var header in extraHeaders)
                request.Headers.Add(header.Item1, header.Item2);
        }

        if (content is not null)
        {
            request.Content = content;
            // request.Headers.TryAddWithoutValidation("Content-Type", MediaTypeNames.Application.Json);
        }

        try
        {
            var (status, response) = await _httpClient.SendAsync(request, token).SuppressCancellationThrow();
            watch.Stop();
            //Log
            return (status, response);
        }
        catch (HttpRequestException ex)
        {
            //Log error
            watch.Stop();

            return (EResult.Abort, GetHttpResponseMessage(ex.Message,
                        ex.StatusCode is not null
                            ? (long)ex.StatusCode
                            : 0,
                        null));
        }
        catch (Exception ex)
        {
            //Log error
            watch.Stop();

            return (EResult.Abort, GetHttpResponseMessage(ex.Message, null, null));
        }
        finally
        {
            request.Dispose();
        }


        //Log watch
    }

    private HttpResponseMessage GetHttpResponseMessage(string content, long? statusCode, Dictionary<string, string> headers)
    {
        var result = new HttpResponseMessage();

        result.Content = new StringContent(content ?? String.Empty);

        if (statusCode != null)
            result.StatusCode = (HttpStatusCode)statusCode;

        if (headers is not null && headers.Any())
            foreach (var header in headers)
                result.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return result;
    }
}