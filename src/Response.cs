using System.Net.Sockets;
using System.Text;

class Response
{
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

    public void Post(Socket client)
    {
        string res = string.Format("{0} {1}\r\n\r\n", HTTPServer.VERSION, status);

        client.Send(ASCIIEncoding.UTF8.GetBytes(res));

    }
}