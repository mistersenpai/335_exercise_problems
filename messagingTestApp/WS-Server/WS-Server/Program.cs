using System.Net.WebSockets;
using System.Text;

var ws = new ClientWebSocket();
string userId;
string recipientId;
while (true)
{
    Console.Write("Input your user ID: ");  // Prompt for your user ID
    userId = Console.ReadLine();

    Console.Write("Input recipient user ID: ");  // Prompt for the recipient user ID
    recipientId = Console.ReadLine();
    break;
}

try
{
    Console.WriteLine("Connecting to server...");
    await ws.ConnectAsync(new Uri($"ws://localhost:8080/ws?id={userId}"), CancellationToken.None);
    Console.WriteLine("Connected!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to server: {ex.Message}");
    return;  // Exit if connection fails
}

var receiveTask = Task.Run(async () =>
{
    var buffer = new byte[1024 * 4];
    while (true)
    {
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Close)
        {
            Console.WriteLine("Connection closed by server");
            break;
        }

        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine($"[CLIENT] Received: {message}");
    }
});

var sendTask = Task.Run(async () =>
{
    while (true)
    {
        var message = Console.ReadLine();

        if (message == "exit")
        {
            break;
        }

        // Send message to the server in the format "recipientId|message"
        var fullMessage = $"{recipientId}|{message}";
        var bytes = Encoding.UTF8.GetBytes(fullMessage);
        await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }
});

await Task.WhenAny(sendTask, receiveTask);

if (ws.State != WebSocketState.Closed)
{
    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
}

await Task.WhenAll(sendTask, receiveTask);
