using System;
using System.Text;

namespace ChessServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set encoding cho Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Set code page (cho Windows)
            try
            {
                Console.OutputEncoding = Encoding.GetEncoding(65001); // UTF-8
            }
            catch
            {
                Console.OutputEncoding = Encoding.UTF8;
            }

            TCPServer server = new TCPServer();
            server.Start(5000);
        }
    }
}
