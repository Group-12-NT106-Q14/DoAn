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
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public bool RegisterUser(string email, string displayName, string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Users (Email, DisplayName, Username, Password, Elo) 
                               VALUES (@Email, @DisplayName, @Username, @Password, 1200)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DisplayName", displayName);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public ClassUser LoginUser(string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"SELECT UserID, Email, DisplayName, Username, Password, Elo 
                               FROM Users 
                               WHERE Username = @Username AND Password = @Password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClassUser(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5)
                            );
                        }
                    }
                }
            }
            return null;
        }
        public bool IsUsernameExists(string username)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public bool IsEmailExists(string email)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public ClassUser GetUserById(int userId)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"SELECT UserID, Email, DisplayName, Username, Password, Elo 
                               FROM Users WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ClassUser(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5)
                            );
                        }
                    }
                }
            }
            return null;
        }
        public bool UpdateUserAccount(int userId, string newDisplayName, string newEmail, string newPassword = null)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query;
                SQLiteCommand cmd;
                if (!string.IsNullOrEmpty(newPassword)) //không đổi mật khẩu
                {
                    string hashedPassword = HashPasswordSHA256(newPassword);
                    query = "UPDATE Users SET DisplayName = @DisplayName, Email = @Email, Password = @Password WHERE UserID = @UserID";
                    cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                }
                else //có đổi mật khẩu
                {
                    query = "UPDATE Users SET DisplayName = @DisplayName, Email = @Email WHERE UserID = @UserID";
                    cmd = new SQLiteCommand(query, conn);
                }
                cmd.Parameters.AddWithValue("@DisplayName", newDisplayName);
                cmd.Parameters.AddWithValue("@Email", newEmail);
                cmd.Parameters.AddWithValue("@UserID", userId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool UpdateElo(int userId, int newElo)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET Elo = @Elo WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Elo", newElo);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public List<ClassUser> GetAllUsers()
        {
            List<ClassUser> users = new List<ClassUser>();
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = @"SELECT UserID, Email, DisplayName, Username, Password, Elo 
                               FROM Users ORDER BY Elo DESC";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new ClassUser(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5)
                            ));
                        }
                    }
                }
            }
            return users;
        }
    }
}
