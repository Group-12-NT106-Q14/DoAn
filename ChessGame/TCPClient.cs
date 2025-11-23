using System.Net.Sockets;
using System.Text;
using System.Text.Json;

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

    // Gửi mà KHÔNG chờ server trả lời (dùng cho GAME_MOVE, GAME_CHAT, ...)
    public void Send(object request)
    {
        string jsonRequest = JsonSerializer.Serialize(request);
        byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
        stream.Write(data, 0, data.Length);
    }

    // Gửi và CHỜ 1 response (dùng cho LOGIN, MATCH_FIND, ROOM_CREATE, ...)
    public string SendRequest(object request)
    {
        string jsonRequest = JsonSerializer.Serialize(request);
        byte[] data = Encoding.UTF8.GetBytes(jsonRequest);
        stream.Write(data, 0, data.Length);
        stream.Flush();

        byte[] buffer = new byte[4096];
        var sb = new StringBuilder();

        // Đọc lần đầu
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        if (bytesRead > 0)
        {
            sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            // Nếu còn data trong stream thì đọc tiếp cho đến khi hết
            while (stream.DataAvailable)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead <= 0) break;
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
        }

        return sb.ToString();
    }


    public void Disconnect()
    {
        stream?.Close();
        client?.Close();
    }

    public NetworkStream GetStream()
    {
        return stream;
    }
}
