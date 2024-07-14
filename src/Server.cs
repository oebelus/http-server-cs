using System.Net;
using System.Net.Sockets;
using System.Text;

namespace codecrafters_http_server.src
{
    class Program
    {
        public static void Main(string[] args)
        {
            HTTPServer server = new(4221);
            Console.WriteLine("Starting server on port 4221!");
            server.Start();
        }
    }
}
