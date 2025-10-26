using System;

namespace ChessServer
{
    // Lớp mô tả thông tin của một người dùng
    public class ClassUser
    {
        // Các thông tin của người chơi
        public int UserID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Elo { get; set; }

        public ClassUser()
        {
            Elo = 1200;
        }
        // Constructor khi lấy từ database
        public ClassUser(int userId, string email, string displayName, string username, string password, int elo)
        {
            UserID = userId;
            Email = email;
            DisplayName = displayName;
            Username = username;
            Password = password;
            Elo = elo;
        }
        // Constructor khi đăng ký người dùng mới
        public ClassUser(string email, string displayName, string username, string password)
        {
            Email = email;
            DisplayName = displayName;
            Username = username;
            Password = password;
            Elo = 1200;
        }
        public override string ToString()
        {
            return $"UserID: {UserID}, Username: {Username}, DisplayName: {DisplayName}, Email: {Email}, Elo: {Elo}";
        }
    }
}
