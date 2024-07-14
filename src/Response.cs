using System.Net.Sockets;

class Response
{
    private byte[]? data = null;
    private string status;

    private Response(string status)
    {
        this.status = status;
    }

    public static Response From(Request request)
    {
        if (request.URL == "/")
            return new Response("200 OK");
        else
            return new Response("404 Not Found");
    }

    public void Post(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);

        writer.WriteLine(string.Format("{0} {1}\r\n\r\n", HTTPServer.VERSION, status));

    }
}