using System.Net.WebSockets;
using TestLicenseManager.Controllers.Services;

namespace TestLicenseManager.Extensions;

public static class WebApplicationExtensions
{
    public static void UseWebCustomSockets(this WebApplication app)
    {
        app.UseWebSockets();

        app.Map("/ws", async (HttpContext context) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await new SocketEchoCommand(webSocket).Execute();
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        });
    }
    
}