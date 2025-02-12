using System.Net.WebSockets;
using System.Text;

public class ServerWebSocket : IDisposable
{
    private readonly ClientWebSocket? _webSocket;
    private readonly Uri              _uri;

    public ServerWebSocket(string uri)
    {
        _uri       = new Uri(uri);
        _webSocket = new ClientWebSocket();
    }

    public void Dispose()
    {
        if (_webSocket != null && _webSocket.State != WebSocketState.None)
        {
            // Закрываем WebSocket, если он открыт
            if (_webSocket.State == WebSocketState.Open)
            {
                _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None).Wait();
            }

            // Освобождаем ресурсы WebSocket
            _webSocket.Dispose();
        }
    }

    public async Task ConnectAsync(CancellationToken token)
    {
        try
        {
            await _webSocket.ConnectAsync(_uri, token);
            Console.WriteLine($"Connected to WebSocket server at {_uri}");

            // Запускаем задачу для получения сообщений
            _ = ReceiveMessagesAsync(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in ConnectAsync: " + ex.Message);
        }
    }

    public async Task SendMessageAsync(string message, CancellationToken token)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);
            await _webSocket.SendAsync(segment, WebSocketMessageType.Text, true, token);
            Console.WriteLine($"Sent message: {message}");
        }
        else
        {
            Console.WriteLine("WebSocket is not open. Cannot send message.");
        }
    }

    private async Task ReceiveMessagesAsync(CancellationToken token)
    {
        byte[] buffer = new byte[1024 * 4];
        try
        {
            while (_webSocket.State == WebSocketState.Open && !token.IsCancellationRequested)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", token);
                    break;
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Received message: " + message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in ReceiveMessagesAsync: " + ex.Message);
        }
        finally
        {
            Dispose();
        }
    }
}