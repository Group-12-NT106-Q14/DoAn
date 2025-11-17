using System;
using System.Text;
using System.Threading;

namespace ChessServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            var server = new TCPServer();
            server.Start(5000);
            Console.WriteLine("Server ready. Nhấn Ctrl+C để thoát.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
