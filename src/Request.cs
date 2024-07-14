class Request
{
    public string Method { get; set; }
    public string URL { get; set; }
    public string Host { get; set; }
    public Dictionary<string, string> Header { get; set; }
    public readonly string Directory;

    private Request(string method, string url, string host, Dictionary<string, string> header, string directory)
    {
        Method = method;
        URL = url;
        Host = host;
        Header = header;
        Directory = directory;
    }

    public static Request? GetRequest(string request, string directory)
    {
        if (string.IsNullOrEmpty(request))
            return null;

        string[] lines = request.Split("\r\n");
        string[] requestLine = lines[0].Split(" ");

        string method = requestLine[0];
        string url = requestLine[1];
        string host = requestLine[2];

        Dictionary<string, string> header = GetHeader(lines);

        return new Request(method, url, host, header, directory);
    }

    private static Dictionary<string, string> GetHeader(string[] lines)
    {
        Dictionary<string, string> headers = [];

        foreach (var item in lines)
        {
            string[] line = item.Split(":");
            if (line.Length > 1)
            {
                headers[line[0]] = line[1].Trim();
            }
        }

        return headers;
    }
}