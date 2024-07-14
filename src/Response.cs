using System.Net.Sockets;
using System.Text;

class Response
{
    private string status;
    private int? contentLength;
    private string? body;
    private string? userAgent;

    public Response(string status)
    {
        this.status = status;
    }

    private Response(string status, int contentLength, string body)
    {
        this.status = status;
        this.contentLength = contentLength;
        this.body = body;
    }

    public static Response From(Request request)
    {
        string[] arr = request.URL.Split('/').Where(x => x != "").ToArray();

        string path = request.URL;

        if (path.StartsWith("/echo")) return new Response("200 OK", arr[1].Length, arr[1]);

        else if (path.StartsWith("/user-agent")) return new Response("200 OK", request.Header["User-Agent"].Length, request.Header["User-Agent"]);

        else if (path == "/") return new Response("200 OK");

        else return new Response("404 Not Found");
    }

    public void Post(Socket client)
    {
        string res;

        if (contentLength == null)
            res = string.Format("{0} {1}\r\n\r\n", HTTPServer.VERSION, status);
        else
            res = string.Format("{0} {1}\r\nContent-Type: text/plain\r\nContent-Length: {2}\r\n\r\n{3}", HTTPServer.VERSION, status, contentLength.ToString(), body);

        client.Send(ASCIIEncoding.UTF8.GetBytes(res));

    }
}