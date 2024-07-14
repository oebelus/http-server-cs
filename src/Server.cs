using System.Net;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_http_server.src
{
    class Program
    {
        public static void Main(string[] args)
        {
            string directory = "";

            if (args.Length > 1 && args[0] == "--directory")
            {
                directory = args[1];
            }

            HTTPServer server = new(4221, directory);
            Console.WriteLine("Starting server on port 4221!");
            server.Start();
        }
    }
}
