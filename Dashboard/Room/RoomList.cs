using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace ChessGame
{
    public partial class RoomList : Form
    {
        // ==== Thông tin người dùng (Dashboard gán trước khi mở form) ====
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int Elo { get; set; }

        // ==== Networking ====
        private readonly TCPClient requestClient = new TCPClient();
        private readonly TCPClient pushClient = new TCPClient();
        private WinFormsTimer uiTimer;
        private readonly StringBuilder pushBuffer = new StringBuilder();

        // ==== Cache danh sách phòng + filter ====
        private List<RoomItem> allRooms = new List<RoomItem>();

        private class RoomItem
        {
            public int Id;
            public string Name;
            public string OwnerDisplay;
            public string OwnerUsername;
            public int OwnerElo;
            public int Players;
            public int Minutes;
            public int Increment;
            public string Status; // "Ngoài sảnh" / "Đang chơi"
        }

        public RoomList()
        {
            InitializeComponent();

            // Gắn events
            Load += RoomList_Load;
            FormClosed += RoomList_FormClosed;

            btnRefresh.Click += (_, __) => ForceReloadRooms();
            btnJoinRoom.Click += (_, __) => TryJoinSelected();
            lstRooms.SelectedIndexChanged += (_, __) => UpdateJoinButtonState();
            lstRooms.DoubleClick += (_, __) => TryJoinSelected();
            txtSearchRoom.TextChanged += (_, __) => ApplyFilter();

            btnCreateRoom.Click += (_, __) => ShowCreateRoomPanel(true);
            btnCancelCreateRoom.Click += (_, __) => ShowCreateRoomPanel(false);
            btnCreateRoomConfirm.Click += BtnCreateRoomConfirm_Click;
        }

        // ==== Lifecycle ====

        private void RoomList_Load(object sender, EventArgs e)
        {
            TryConnectSilently(requestClient);
            TryConnectSilently(pushClient);

            // Gắn phiên cho từng kênh
            requestClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "req" });
            pushClient.SendRequest(new { action = "AUTH_ATTACH", username = this.Username, mode = "push" });

            // Nếu Dashboard chưa truyền info thì hỏi lại server
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(DisplayName) || Elo <= 0)
            {
                try
                {
                    var res = requestClient.SendRequest(new { action = "GET_USER_INFO" });
                    using var doc = JsonDocument.Parse(res);
                    if (doc.RootElement.TryGetProperty("user", out var u) && u.ValueKind != JsonValueKind.Null)
                    {
                        Username = u.GetProperty("username").GetString();
                        DisplayName = u.GetProperty("displayName").GetString();
                        Elo = u.GetProperty("elo").GetInt32();
                    }
                }
                catch { }
            }

            lblTitle.Text = "DANH SÁCH PHÒNG";

            // Lấy danh sách ban đầu
            ForceReloadRooms();

            // Timer đọc push type "ROOMS"
            uiTimer = new WinFormsTimer();
            uiTimer.Interval = 50;
            uiTimer.Tick += UiTimer_Tick;
            uiTimer.Start();
        }

        private void RoomList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { uiTimer?.Stop(); } catch { }
            try { pushClient?.Disconnect(); } catch { }
            try { requestClient?.Disconnect(); } catch { }
        }

        private void TryConnectSilently(TCPClient c)
        {
            try { c.Connect(); } catch { }
        }

        // ==== Panel tạo phòng nhỏ ====

        private void ShowCreateRoomPanel(bool show)
        {
            panelCreateRoom.Visible = show;
            if (show)
            {
                txtRoomName.Clear();
                txtRoomName.Focus();
                panelCreateRoom.BringToFront();
            }
        }

        private void BtnCreateRoomConfirm_Click(object sender, EventArgs e)
        {
            var name = (txtRoomName.Text ?? "").Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên phòng.");
                return;
            }

            try
            {
                var res = requestClient.SendRequest(new { action = "ROOM_CREATE", roomName = name });
                using var doc = JsonDocument.Parse(res);
                bool ok = doc.RootElement.GetProperty("success").GetBoolean();
                string message = doc.RootElement.GetProperty("message").GetString();
                if (!ok)
                {
                    MessageBox.Show(message);
                    return;
                }

                var room = doc.RootElement.GetProperty("room");
                int newRoomId = room.GetProperty("id").GetInt32();
                string newRoomName = room.GetProperty("name").GetString();

                // Ẩn panel tạo
                panelCreateRoom.Visible = false;

                // Ẩn RoomList, mở InRoom dạng dialog, xong quay lại + reload
                this.Hide();
                try
                {
                    using (var f = new InRoom(newRoomId, newRoomName, true)
                    {
                        Username = this.Username,
                        DisplayName = this.DisplayName,
                        Elo = this.Elo
                    })
                    {
                        f.ShowDialog(this);
                    }
                }
                finally
                {
                    this.Show();
                    ForceReloadRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tạo được phòng. " + ex.Message);
            }
        }

        // ==== Join phòng ====

        private RoomItem GetSelectedRoom()
        {
            if (lstRooms.SelectedItems.Count == 0) return null;
            var li = lstRooms.SelectedItems[0];
            return li.Tag as RoomItem;
        }

        private void TryJoinSelected()
        {
            var sel = GetSelectedRoom();
            if (sel == null) return;

            if (sel.Players >= 2)
            {
                MessageBox.Show("Phòng đã đầy (2/2).");
                return;
            }

            try
            {
                var res = requestClient.SendRequest(new { action = "ROOM_JOIN", roomId = sel.Id });
                using var doc = JsonDocument.Parse(res);
                bool ok = doc.RootElement.GetProperty("success").GetBoolean();
                string message = doc.RootElement.GetProperty("message").GetString();
                if (!ok)
                {
                    MessageBox.Show(message);
                    return;
                }

                // Ẩn RoomList, mở InRoom dạng dialog, xong quay lại + reload
                this.Hide();
                try
                {
                    using (var f = new InRoom(sel.Id, sel.Name, false)
                    {
                        Username = this.Username,
                        DisplayName = this.DisplayName,
                        Elo = this.Elo
                    })
                    {
                        f.ShowDialog(this);
                    }
                }
                finally
                {
                    this.Show();
                    ForceReloadRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không vào được phòng. " + ex.Message);
            }
        }

        private void UpdateJoinButtonState()
        {
            var sel = GetSelectedRoom();
            btnJoinRoom.Enabled = (sel != null) && (sel.Players < 2);
        }

        // ==== Load danh sách từ server ====

        private void ForceReloadRooms()
        {
            try
            {
                var res = requestClient.SendRequest(new { action = "ROOM_LIST" });
                using var doc = JsonDocument.Parse(res);
                if (doc.RootElement.GetProperty("success").GetBoolean())
                {
                    allRooms = doc.RootElement.GetProperty("rooms")
                                              .EnumerateArray()
                                              .Select(ToItem)
                                              .ToList();
                    ApplyFilter();
                }
            }
            catch
            {
            }
        }

        private RoomItem ToItem(JsonElement r)
        {
            var item = new RoomItem
            {
                Id = r.GetProperty("id").GetInt32(),
                Name = r.GetProperty("name").GetString(),
                OwnerDisplay = r.GetProperty("ownerDisplayName").GetString(),
                OwnerUsername = r.GetProperty("ownerUsername").GetString(),
                OwnerElo = r.GetProperty("ownerElo").GetInt32(),
                Players = r.GetProperty("players").GetInt32(),
                Minutes = r.GetProperty("minutes").GetInt32(),
                Increment = r.GetProperty("increment").GetInt32()
            };

            // Trạng thái: đọc nếu server có gửi; nếu không thì mặc định "Ngoài sảnh"
            string statusText = "Ngoài sảnh";

            if (r.TryGetProperty("status", out var js) && js.ValueKind == JsonValueKind.String)
            {
                string s = js.GetString();
                if (string.Equals(s, "playing", StringComparison.OrdinalIgnoreCase))
                    statusText = "Đang chơi";
                else if (string.Equals(s, "lobby", StringComparison.OrdinalIgnoreCase))
                    statusText = "Ngoài sảnh";
                else
                    statusText = s;
            }
            else if (r.TryGetProperty("isPlaying", out var jp) &&
                     (jp.ValueKind == JsonValueKind.True || jp.ValueKind == JsonValueKind.False))
            {
                bool playing = jp.GetBoolean();
                statusText = playing ? "Đang chơi" : "Ngoài sảnh";
            }

            item.Status = statusText;
            return item;
        }

        // ==== Filter + render ListView ====

        private void ApplyFilter()
        {
            string q = (txtSearchRoom.Text ?? "").Trim();
            int? keepId = null;
            var sel = GetSelectedRoom();
            if (sel != null) keepId = sel.Id;

            IEnumerable<RoomItem> view = allRooms;
            if (!string.IsNullOrEmpty(q))
            {
                var qq = q.ToLowerInvariant();
                view = view.Where(r =>
                    (r.Name ?? "").ToLowerInvariant().Contains(qq) ||
                    (r.OwnerDisplay ?? "").ToLowerInvariant().Contains(qq) ||
                    (r.OwnerUsername ?? "").ToLowerInvariant().Contains(qq)
                );
            }

            lstRooms.BeginUpdate();
            lstRooms.Items.Clear();
            foreach (var it in view)
            {
                var li = new ListViewItem(it.Name);
                li.SubItems.Add(it.OwnerDisplay);
                li.SubItems.Add(it.OwnerElo.ToString());
                li.SubItems.Add($"{it.Players}/2");
                li.SubItems.Add(it.Status);
                li.Tag = it;
                lstRooms.Items.Add(li);
            }
            lstRooms.EndUpdate();

            // Giữ lại phòng đang chọn nếu còn trong danh sách
            if (keepId.HasValue)
            {
                foreach (ListViewItem li in lstRooms.Items)
                {
                    if (li.Tag is RoomItem ri && ri.Id == keepId.Value)
                    {
                        li.Selected = true;
                        li.EnsureVisible();
                        break;
                    }
                }
            }

            UpdateJoinButtonState();
        }

        // ==== Nhận push từ server (ROOMS) ====

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            var stream = pushClient.GetStream();
            if (stream == null) return;

            try
            {
                while (stream.DataAvailable)
                {
                    byte[] buf = new byte[4096];
                    int n = stream.Read(buf, 0, buf.Length);
                    if (n <= 0) break;
                    pushBuffer.Append(Encoding.UTF8.GetString(buf, 0, n));
                }

                foreach (var json in ExtractJsonObjects(pushBuffer))
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        var root = doc.RootElement;
                        if (!root.TryGetProperty("type", out var jt)) continue;
                        var type = jt.GetString();
                        if (type == "ROOMS")
                        {
                            allRooms = root.GetProperty("rooms").EnumerateArray().Select(ToItem).ToList();
                            ApplyFilter();
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private static IEnumerable<string> ExtractJsonObjects(StringBuilder sb)
        {
            var list = new List<string>();
            int depth = 0, start = -1;
            for (int i = 0; i < sb.Length; i++)
            {
                char c = sb[i];
                if (c == '{')
                {
                    if (depth == 0) start = i;
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0 && start != -1)
                    {
                        list.Add(sb.ToString(start, i - start + 1));
                        start = -1;
                    }
                }
            }

            if (list.Count > 0)
            {
                string last = list[list.Count - 1];
                int idx = sb.ToString().LastIndexOf(last, StringComparison.Ordinal);
                if (idx >= 0) sb.Remove(0, idx + last.Length);
            }

            return list;
        }
    }
}
