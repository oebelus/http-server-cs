class Request
{
    public string Method { get; set; }
    public string URL { get; set; }
    public string Host { get; set; }
    byte[] buffer = new byte[1024];

    private Request(string method, string url, string host)
    {
        Method = method;
        URL = url;
        Host = host;
    }

    public static Request? GetRequest(string request)
    {
        if (string.IsNullOrEmpty(request))
            return null;

        string[] lines = request.Split("\r\n");
        string[] requestLine = lines[0].Split(" ");

        string method = requestLine[0];
        string url = requestLine[1];
        string host = requestLine[2];

        return new Request(method, url, host);
    }
}