namespace ClientTest;

[TestFixture]
public class SocketTests
{
    private ServerWebSocket?         _serverSocket;
    private CancellationTokenSource? _cancellationTokenSource;

    [OneTimeSetUp]
    public void Setup()
    {
        _serverSocket ??= new ServerWebSocket("wss://localhost:7133/ws");

        _cancellationTokenSource = new();
    }

    [Test]
    public async Task A_Connect()
    {
        await _serverSocket.ConnectAsync(_cancellationTokenSource.Token);
        
        await _serverSocket.SendMessageAsync("Hello, Server!", _cancellationTokenSource.Token);

        await Task.Delay(5000);
    }
    
    [Test]
    public async Task B_Cancellation()
    {
        await _serverSocket.ConnectAsync(_cancellationTokenSource.Token);
        _cancellationTokenSource.Cancel();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _serverSocket.Dispose();
        _cancellationTokenSource.Cancel();
    }
}