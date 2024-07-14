using System.Net;
using System.Net.Sockets;
using System.Text;

class HTTPServer(int port)
{
    private readonly TcpListener server = new(IPAddress.Any, port);
    bool running = false;
    public const string VERSION = "HTTP/1.1";

    public void Start()
    {
        while (true)
        {
            Thread serverThread = new(new ThreadStart(Run));
            serverThread.Start();
        }
    }

    private void Run()
    {
        server.Start();
        Console.WriteLine("Waiting for connection...");

        Socket client = server.AcceptSocket();

        Console.WriteLine("Client connected!");

        HandleClient(client);

        client.Close();

    }

    private static void HandleClient(Socket client)
    {
        byte[] buffer = new byte[1024];
        int receivedBytes = client.Receive(buffer);
        string requestString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

        Console.WriteLine(requestString);

        Request? req = Request.GetRequest(requestString);

        Response? res = Response.From(req);
        res.Post(client);
    }
}