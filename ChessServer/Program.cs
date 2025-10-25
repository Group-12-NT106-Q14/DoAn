using System;
using System.Text;

namespace ChessServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            TCPServer server = new TCPServer();
            server.Start(5000);
        }
    }
}
