using System.Net.Sockets;
using System.Text;

class Response
{
    private string status;
    private int? contentLength;
    private string? contentType;
    private string? body;

    public Response(string status)
    {
        this.status = status;
    }

    private Response(string status, string contentType, int contentLength, string body)
    {
        this.status = status;
        this.contentLength = contentLength;
        this.contentType = contentType;
        this.body = body;
    }

    public static Response From(Request request)
    {
        string[] arr = request.URL.Split('/').Where(x => x != "").ToArray();

        string path = request.URL;

        if (path.StartsWith("/echo")) return new Response("200 OK", "text/plain", arr[1].Length, arr[1]);

        else if (path.StartsWith("/user-agent")) return new Response("200 OK", "text/plain", request.Header["User-Agent"].Length, request.Header["User-Agent"]);

        else if (path.StartsWith("/files"))
        {
            string dir = request.Directory + path[7..];

            if (File.Exists(dir))
                return new Response("200 OK", "application/octet-stream", File.ReadAllText(dir).Length, File.ReadAllText(dir));
            else
                return new Response("404 Not Found");

        }

        else if (path == "/") return new Response("200 OK");

        else return new Response("404 Not Found");
    }

    public void Post(Socket client)
    {
        string res;

        if (contentLength == null)
            res = string.Format("{0} {1}\r\n\r\n", HTTPServer.VERSION, status);
        else
            res = string.Format("{0} {1}\r\nContent-Type: {2}\r\nContent-Length: {3}\r\n\r\n{4}", HTTPServer.VERSION, status, contentType, contentLength.ToString(), body);

        client.Send(ASCIIEncoding.UTF8.GetBytes(res));
    }
}