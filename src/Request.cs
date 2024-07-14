class Request
{
    public string Method { get; set; }
    public string URL { get; set; }
    public string Host { get; set; }

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

        string[] tokens = request.Split(" ");

        string method = tokens[0];
        string url = tokens[1];
        string host = tokens[2];

        return new Request(method, url, host);
    }
}