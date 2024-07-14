using System.Net;
using System.Net.Sockets;

class HTTPServer(int port)
{
    private readonly int port;
    private readonly TcpListener server = new(IPAddress.Any, port);
    bool running = false;
    public const string VERSION = "HTTP/1.1";

    public void Start()
    {
        Thread serverThread = new(new ThreadStart(Run));
        serverThread.Start();
    }

    private void Run()
    {
        running = true;
        server.Start();

        while (running)
        {
            Console.WriteLine("Waiting for connection...");

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("Client connected!");

            HandleClient(client);

            client.Close();
        }

        server.Stop();
    }

    private static void HandleClient(TcpClient client)
    {
        StreamReader reader = new(client.GetStream());

        string msg = "";

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (line == null) break;
            msg += line + "\n";
        }

        Console.WriteLine("Request: \n" + msg);

        Request? req = Request.GetRequest(msg);
        Response? res = Response.From(req);
        res.Post(client);
    }
}