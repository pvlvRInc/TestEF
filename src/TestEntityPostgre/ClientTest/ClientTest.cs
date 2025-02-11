using FluentAssertions;
using LicenseManagerClient.Extensions;
using LicenseManagerClient.Services.Api.Base;

namespace ClientTest;

[TestFixture]
public class Tests
{
    private ServerConnection? _serverConnection;
    private ServerHttpClient? _httpClient;

    [OneTimeSetUp]
    public void Setup()
    {
        _httpClient ??= new ServerHttpClient("https://localhost:7133/");
        _serverConnection ??= new ServerConnection(_httpClient, "LocalHost");
        
        _serverConnection.Connect();
    }

    [Test]
    public async Task LoginTest()
    {
        
        // Arrange
        var testUsername = "pes@sutuli.com";
        var testPassword = "!Ss123456";


        // Act
        var (result, response, resultData) = await _serverConnection.Login(testUsername, testPassword);

        Console.WriteLine($"Result: {result} | Response message: {response.message.ToNiceJson()} | Token: {resultData.accessToken}");
        
        // Assert
        _serverConnection.Token.Should().NotBeEmpty().And.NotBeNull();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _httpClient.Dispose();
    }
}