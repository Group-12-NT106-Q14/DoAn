using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;

public class TCPClient
{
    private TcpClient client;
    private NetworkStream stream;
    private string serverIP;
    private int serverPort;

    private readonly object sendLock = new object();

    // Timer heartbeat chạy nền
    private System.Timers.Timer heartbeatTimer;
    private DateTime lastSendTime;
    private bool isConnected;

    private const int HEARTBEAT_INTERVAL_MS = 10000;              // 10s: mỗi 10s tick 1 lần
    private const int HEARTBEAT_IDLE_THRESHOLD_SECONDS = 10;      // Im lặng >= 10s mới gửi heartbeat

    // Đảm bảo chỉ xử lý disconnect 1 lần
    private static bool disconnectHandled = false;

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
        isConnected = true;
        lastSendTime = DateTime.UtcNow;
        StartHeartbeat();
        return true;
    }

    // Gửi mà KHÔNG chờ server trả lời (dùng cho GAME_MOVE, GAME_CHAT, ...)
    public void Send(object request)
    {
        if (stream == null) throw new InvalidOperationException("Chưa kết nối tới server.");

        try
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(jsonRequest);

            lock (sendLock)
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            lastSendTime = DateTime.UtcNow;
        }
        catch (Exception)
        {
            HandleDisconnect("Không thể gửi dữ liệu tới server.");
        }
    }

    // Gửi và CHỜ 1 response (dùng cho LOGIN, MATCH_FIND, ROOM_CREATE, ...)
    public string SendRequest(object request)
    {
        if (stream == null) throw new InvalidOperationException("Chưa kết nối tới server.");

        try
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(jsonRequest);

            lock (sendLock)
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            lastSendTime = DateTime.UtcNow;

            byte[] buffer = new byte[4096];
            var sb = new StringBuilder();

            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                while (stream.DataAvailable)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0) break;
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }
            }

            return sb.ToString();
        }
        catch (Exception)
        {
            // Mất kết nối trong lúc gửi/nhận => hiện popup + quay lại login
            HandleDisconnect("Không thể gửi/nhận dữ liệu từ server.");
            return JsonSerializer.Serialize(new { success = false, message = "Mất kết nối tới server." });
        }
    }

    public void Disconnect()
    {
        isConnected = false;

        if (heartbeatTimer != null)
        {
            try
            {
                heartbeatTimer.Stop();
                heartbeatTimer.Dispose();
            }
            catch
            {
            }
            heartbeatTimer = null;
        }

        try { stream?.Close(); } catch { }
        try { client?.Close(); } catch { }

        stream = null;
        client = null;
    }

    public NetworkStream GetStream()
    {
        return stream;
    }

    // ================== HEARTBEAT NỘI BỘ ==================

    private void StartHeartbeat()
    {
        if (heartbeatTimer != null)
        {
            try
            {
                heartbeatTimer.Stop();
                heartbeatTimer.Dispose();
            }
            catch
            {
            }
            heartbeatTimer = null;
        }

        // Timer của System.Timers
        heartbeatTimer = new System.Timers.Timer(HEARTBEAT_INTERVAL_MS);
        heartbeatTimer.AutoReset = true;
        heartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
        heartbeatTimer.Start();
    }

    private void HeartbeatTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            if (!isConnected) return;
            if (client == null || !client.Connected) throw new Exception("Socket đã đóng.");
            if (stream == null || !stream.CanWrite) throw new Exception("Stream không còn khả dụng.");

            // Nếu mới gửi request gì đó < 10s thì không cần ping nữa (tránh spam)
            if ((DateTime.UtcNow - lastSendTime).TotalSeconds < HEARTBEAT_IDLE_THRESHOLD_SECONDS)
                return;

            var heartbeatObj = new { action = "HEARTBEAT" };
            string json = JsonSerializer.Serialize(heartbeatObj);
            byte[] data = Encoding.UTF8.GetBytes(json);

            lock (sendLock)
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            lastSendTime = DateTime.UtcNow;
        }
        catch
        {
            // Heartbeat lỗi (server đã đóng kết nối / mạng chết) => auto logout
            HandleDisconnect("Kết nối tới server bị gián đoạn (heartbeat).");
        }
    }

    private void HandleDisconnect(string reason)
    {
        if (disconnectHandled) return;
        disconnectHandled = true;

        try
        {
            Disconnect();
        }
        catch { }

        try
        {
            MessageBox.Show(
                "Kết nối tới server đã bị mất.\n" + reason + "\nBạn sẽ được đưa về màn hình đăng nhập.",
                "Disconnected",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
        catch { }

        try
        {
            // Restart app => Program.cs chạy lại => mở form Login
            Application.Restart();
        }
        catch
        {
            Environment.Exit(0);
        }
    }
}
