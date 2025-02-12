using System.Net.WebSockets;
using TestLicenseManager.Extensions;

namespace TestLicenseManager.Controllers.Services;

public class SocketEchoCommand : IDisposable
{
    private readonly WebSocket _socket;

    public SocketEchoCommand(WebSocket socket) =>
        _socket = socket;

    public void Dispose() =>
        _socket?.Dispose();

    public async Task Execute()
    {
        var buffer = new byte[1024 * 4];

        WebSocketReceiveResult result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        try
        {
            while (!result.CloseStatus.HasValue)
            {
                await _socket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
        }
        catch (WebSocketException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        finally
        {
            if (result.CloseStatus.HasValue)
                await _socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }


        Dispose();
    }
}