using System.Net;
using System.Net.Sockets;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

TcpListener server = new(IPAddress.Any, 4221);
server.Start();
server.AcceptSocket(); // wait for client
