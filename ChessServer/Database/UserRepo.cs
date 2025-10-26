using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace ChessServer
{
    public class UserRepo
    {
        private string HashPasswordSHA256(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha.ComputeHash(bytes);
            string result = "";
            for (int i = 0; i < hash.Length; i++)
            {
                result += hash[i].ToString("x2");
            }
            return result;
        }
        // Đăng ký user mới
        public bool RegisterUser(string email, string displayName, string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string query = "INSERT INTO Users (Email, DisplayName, Username, Password, Elo) VALUES (@Email, @DisplayName, @Username, @Password, 1200)";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@DisplayName", displayName);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            return rows > 0;
        }
        // Đăng nhập
        public ClassUser LoginUser(string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT UserID, Email, DisplayName, Username, Password, Elo FROM Users WHERE Username = @Username AND Password = @Password";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            SQLiteDataReader reader = cmd.ExecuteReader();
            ClassUser user = null;
            if (reader.Read())
            {
                user = new ClassUser();
                user.UserID = reader.GetInt32(0);
                user.Email = reader.GetString(1);
                user.DisplayName = reader.GetString(2);
                user.Username = reader.GetString(3);
                user.Password = reader.GetString(4);
                user.Elo = reader.GetInt32(5);
            }
            reader.Close();
            conn.Close();
            return user;
        }
        // Đặt lại mật khẩu theo email
        public bool ResetPassword(string email, string newPassword)
        {
            string hashed = HashPasswordSHA256(newPassword);
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string query = "UPDATE Users SET Password = @Password WHERE Email = @Email";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Password", hashed);
            cmd.Parameters.AddWithValue("@Email", email);
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            return rows > 0;
        }
        // Kiểm tra username đã tồn tại chưa
        public bool IsUsernameExists(string username)
        {
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (count > 0) return true;
            return false;
        }
        // Kiểm tra email đã tồn tại
        public bool IsEmailExists(string email)
        {
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (count > 0) return true;
            return false;
        }
        // Lấy user theo ID
        public ClassUser GetUserById(int userId)
        {
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT UserID, Email, DisplayName, Username, Password, Elo FROM Users WHERE UserID = @UserID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", userId);
            SQLiteDataReader reader = cmd.ExecuteReader();
            ClassUser user = null;
            if (reader.Read())
            {
                user = new ClassUser();
                user.UserID = reader.GetInt32(0);
                user.Email = reader.GetString(1);
                user.DisplayName = reader.GetString(2);
                user.Username = reader.GetString(3);
                user.Password = reader.GetString(4);
                user.Elo = reader.GetInt32(5);
            }
            reader.Close();
            conn.Close();
            return user;
        }
        // Cập nhật tài khoản người dùng
        public bool UpdateUserAccount(int userId, string newDisplayName, string newEmail, string newPassword = null)
        {
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            SQLiteCommand cmd;
            if (newPassword != null && newPassword != "")
            {
                string hashed = HashPasswordSHA256(newPassword);
                string sql = "UPDATE Users SET DisplayName = @DisplayName, Email = @Email, Password = @Password WHERE UserID = @UserID";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Password", hashed);
            }
            else
            {
                string sql = "UPDATE Users SET DisplayName = @DisplayName, Email = @Email WHERE UserID = @UserID";
                cmd = new SQLiteCommand(sql, conn);
            }
            cmd.Parameters.AddWithValue("@DisplayName", newDisplayName);
            cmd.Parameters.AddWithValue("@Email", newEmail);
            cmd.Parameters.AddWithValue("@UserID", userId);
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            return rows > 0;
        }
        // Cập nhật điểm Elo
        public bool UpdateElo(int userId, int newElo)
        {
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "UPDATE Users SET Elo = @Elo WHERE UserID = @UserID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Elo", newElo);
            cmd.Parameters.AddWithValue("@UserID", userId);
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            return rows > 0;
        }
        // Lấy danh sách user
        public List<ClassUser> GetAllUsers()
        {
            List<ClassUser> users = new List<ClassUser>();
            SQLiteConnection conn = Database.GetConnection();
            conn.Open();
            string sql = "SELECT UserID, Email, DisplayName, Username, Password, Elo FROM Users ORDER BY Elo DESC";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ClassUser u = new ClassUser();
                u.UserID = reader.GetInt32(0);
                u.Email = reader.GetString(1);
                u.DisplayName = reader.GetString(2);
                u.Username = reader.GetString(3);
                u.Password = reader.GetString(4);
                u.Elo = reader.GetInt32(5);
                users.Add(u);
            }
            reader.Close();
            conn.Close();
            return users;
        }
    }
}