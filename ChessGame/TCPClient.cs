using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ChessGame
{
    public class TCPClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private string serverIP;
        private int serverPort;
        public TCPClient(string serverIP = "127.0.0.1", int serverPort = 5000)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }
        public bool Connect()
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();
            return true;
        }
        public string SendRequest(object request)
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[4096];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            return response;
        }
        public void Disconnect()
        {
            stream?.Close();
            client?.Close();
        }
    }
}
