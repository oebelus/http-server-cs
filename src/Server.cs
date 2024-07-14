using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

TcpListener server = new(IPAddress.Any, 4221);

server.Start();
Socket client = server.AcceptSocket(); // wait for client

string httpResponse = "HTTP/1.1 200 OK\r\n\r\n";

client.Send(Encoding.UTF8.GetBytes(httpResponse));
