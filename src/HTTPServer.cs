using System.Net;
using System.Net.Sockets;
using System.Text;

class HTTPServer(int port, string directory)
{
    private readonly TcpListener server = new TcpListener(IPAddress.Any, port);
    public const string VERSION = "HTTP/1.1";
    private readonly string directory = directory;

    public void Start()
    {
        while (true)
        {
            server.Start();
            Socket client = server.AcceptSocket();
            Task.Run(() => HandleClient(client));
        }
    }

    private void HandleClient(Socket client)
    {
        byte[] buffer = new byte[1024];
        int receivedBytes = client.Receive(buffer);
        string requestString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

        Console.WriteLine(requestString);

        Request? req = Request.GetRequest(requestString, directory);

        Response? res = Response.From(req!);
        res.Post(client);
    }
}