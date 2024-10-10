using System.Net.WebSockets;
using System.Text;

var ws = new ClientWebSocket();
string name;
string room;
while (true)
{
    Console.Write("Input name: ");
    name = Console.ReadLine();

    Console.Write("input room:");
    room = Console.ReadLine();
    break;
}

try
{
    Console.WriteLine("Connecting to server...");
    await ws.ConnectAsync(new Uri($"ws://localhost:8080/ws?name={name}&room={room}"), CancellationToken.None);  // Updated URL
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
        var result = await ws.ReceiveAsync(
            new ArraySegment<byte>(buffer),
            CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Close)
        {
            Console.WriteLine("Connection closed by server");
            break;
        }

        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine($"[CLIENT] Received from server: {message}");
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

        var bytes = Encoding.UTF8.GetBytes(message);
        await ws.SendAsync(new ArraySegment<byte>(bytes),
            WebSocketMessageType.Text, true,
            CancellationToken.None);
    }
});

await Task.WhenAny(sendTask, receiveTask);

if (ws.State != WebSocketState.Closed)
{
    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
}

await Task.WhenAll(sendTask, receiveTask);
