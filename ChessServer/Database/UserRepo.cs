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
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Đăng ký user mới
        public bool RegisterUser(string email, string displayName, string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);

            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SQLiteTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        string query =
                            "INSERT INTO Users (Email, DisplayName, Username, Password, Elo) " +
                            "VALUES (@Email, @DisplayName, @Username, @Password, 1200)";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@DisplayName", displayName);
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", hashedPassword);
                            int rows = cmd.ExecuteNonQuery();
                            if (rows <= 0)
                            {
                                tran.Rollback();
                                return false;
                            }
                        }

                        long newUserId = conn.LastInsertRowId;

                        // Khởi tạo UserStats cho user mới
                        string statsSql =
                            "INSERT INTO UserStats (UserID, GamesPlayed, Wins, Draws, Losses, BestElo, LastActive) " +
                            "VALUES (@UserID, 0, 0, 0, 0, 1200, NULL)";

                        using (SQLiteCommand statsCmd = new SQLiteCommand(statsSql, conn, tran))
                        {
                            statsCmd.Parameters.AddWithValue("@UserID", (int)newUserId);
                            statsCmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        // Đăng nhập
        public ClassUser LoginUser(string username, string password)
        {
            string hashedPassword = HashPasswordSHA256(password);

            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT UserID, Email, DisplayName, Username, Password, Elo " +
                    "FROM Users WHERE Username = @Username AND Password = @Password";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
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
                        return user;
                    }
                }
            }
        }

        // Đặt lại mật khẩu theo email
        public bool ResetPassword(string email, string newPassword)
        {
            string hashed = HashPasswordSHA256(newPassword);

            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET Password = @Password WHERE Email = @Email";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", hashed);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Kiểm tra username đã tồn tại chưa
        public bool IsUsernameExists(string username)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Kiểm tra email đã tồn tại chưa
        public bool IsEmailExists(string email)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Lấy user theo ID
        public ClassUser GetUserById(int userId)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT UserID, Email, DisplayName, Username, Password, Elo " +
                    "FROM Users WHERE UserID = @UserID";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
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
                        return user;
                    }
                }
            }
        }

        // Cập nhật tài khoản người dùng
        // newPassword = null hoặc rỗng thì không đổi mật khẩu
        public bool UpdateUserAccount(int userId, string newDisplayName, string newEmail, string newPassword = null)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();

                if (!string.IsNullOrEmpty(newPassword))
                {
                    string hashed = HashPasswordSHA256(newPassword);
                    string query =
                        "UPDATE Users " +
                        "SET DisplayName = @DisplayName, Email = @Email, Password = @Password " +
                        "WHERE UserID = @UserID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DisplayName", newDisplayName);
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@Password", hashed);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
                else
                {
                    string query =
                        "UPDATE Users " +
                        "SET DisplayName = @DisplayName, Email = @Email " +
                        "WHERE UserID = @UserID";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DisplayName", newDisplayName);
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
        }

        // Cập nhật Elo hiện tại cho user
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
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Lấy danh sách tất cả user
        public List<ClassUser> GetAllUsers()
        {
            List<ClassUser> users = new List<ClassUser>();

            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql =
                    "SELECT UserID, Email, DisplayName, Username, Password, Elo " +
                    "FROM Users";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
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
                }
            }

            return users;
        }

        // Lưu kết quả một ván cờ và cập nhật UserStats cho hai người chơi
        // result: 0 = trắng thắng, 1 = đen thắng, 2 = hòa
        public void SaveMatch(
            int whiteUserId,
            int blackUserId,
            int result,
            DateTime startTime,
            DateTime endTime,
            int timeControlMinutes,
            int incrementSeconds,
            int whiteEloBefore,
            int whiteEloAfter,
            int blackEloBefore,
            int blackEloAfter,
            string movesPgn = null)
        {
            using (SQLiteConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SQLiteTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        string sql =
                            "INSERT INTO Matches (" +
                            "WhiteUserID, BlackUserID, Result, StartTime, EndTime, " +
                            "TimeControlMinutes, IncrementSeconds, " +
                            "WhiteEloBefore, WhiteEloAfter, BlackEloBefore, BlackEloAfter, MovesPGN) " +
                            "VALUES (" +
                            "@WhiteUserID, @BlackUserID, @Result, @StartTime, @EndTime, " +
                            "@TimeControlMinutes, @IncrementSeconds, " +
                            "@WhiteEloBefore, @WhiteEloAfter, @BlackEloBefore, @BlackEloAfter, @MovesPGN)";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@WhiteUserID", whiteUserId);
                            cmd.Parameters.AddWithValue("@BlackUserID", blackUserId);
                            cmd.Parameters.AddWithValue("@Result", result);
                            cmd.Parameters.AddWithValue("@StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmd.Parameters.AddWithValue("@EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            cmd.Parameters.AddWithValue("@TimeControlMinutes", timeControlMinutes);
                            cmd.Parameters.AddWithValue("@IncrementSeconds", incrementSeconds);
                            cmd.Parameters.AddWithValue("@WhiteEloBefore", whiteEloBefore);
                            cmd.Parameters.AddWithValue("@WhiteEloAfter", whiteEloAfter);
                            cmd.Parameters.AddWithValue("@BlackEloBefore", blackEloBefore);
                            cmd.Parameters.AddWithValue("@BlackEloAfter", blackEloAfter);
                            if (string.IsNullOrEmpty(movesPgn))
                                cmd.Parameters.AddWithValue("@MovesPGN", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@MovesPGN", movesPgn);

                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật thống kê cho từng user
                        UpdateUserStatsInternal(conn, tran, whiteUserId, whiteEloAfter, result == 0, result == 2, endTime);
                        UpdateUserStatsInternal(conn, tran, blackUserId, blackEloAfter, result == 1, result == 2, endTime);

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Cập nhật UserStats cho 1 user trong 1 ván
        // isWin / isDraw quyết định cách cộng Win/Draw/Loss
        private void UpdateUserStatsInternal(SQLiteConnection conn, SQLiteTransaction tran,
            int userId, int newElo, bool isWin, bool isDraw, DateTime endTime)
        {
            // Đảm bảo đã có dòng UserStats cho user này
            string insertSql =
                "INSERT OR IGNORE INTO UserStats (UserID, GamesPlayed, Wins, Draws, Losses, BestElo, LastActive) " +
                "VALUES (@UserID, 0, 0, 0, 0, @BestElo, NULL)";

            using (SQLiteCommand insertCmd = new SQLiteCommand(insertSql, conn, tran))
            {
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.Parameters.AddWithValue("@BestElo", newElo);
                insertCmd.ExecuteNonQuery();
            }

            int addWin = isWin ? 1 : 0;
            int addDraw = isDraw ? 1 : 0;
            int addLoss = (!isWin && !isDraw) ? 1 : 0;
            string endTimeStr = endTime.ToString("yyyy-MM-dd HH:mm:ss");

            string updateSql =
                "UPDATE UserStats SET " +
                "GamesPlayed = GamesPlayed + 1, " +
                "Wins = Wins + @AddWin, " +
                "Draws = Draws + @AddDraw, " +
                "Losses = Losses + @AddLoss, " +
                "BestElo = CASE WHEN BestElo < @NewElo THEN @NewElo ELSE BestElo END, " +
                "LastActive = @LastActive " +
                "WHERE UserID = @UserID";

            using (SQLiteCommand updateCmd = new SQLiteCommand(updateSql, conn, tran))
            {
                updateCmd.Parameters.AddWithValue("@AddWin", addWin);
                updateCmd.Parameters.AddWithValue("@AddDraw", addDraw);
                updateCmd.Parameters.AddWithValue("@AddLoss", addLoss);
                updateCmd.Parameters.AddWithValue("@NewElo", newElo);
                updateCmd.Parameters.AddWithValue("@LastActive", endTimeStr);
                updateCmd.Parameters.AddWithValue("@UserID", userId);
                updateCmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Lưu kết quả trận đấu, cập nhật Elo + UserStats + Matches (schema mới).
        /// result: "white" / "black" / "draw"
        /// reason: "checkmate", "resign", "time", "agreement", ...
        /// Quy tắc Elo: win +20, lose -20, draw 0.
        /// timeControlMinutes, incrementSeconds: thời gian ván cờ (phút + increment).
        /// </summary>
        public void SaveMatchAndUpdateStats(
            int whiteUserId,
            int blackUserId,
            string result,
            string reason,
            int timeControlMinutes,
            int incrementSeconds)
        {
            result = (result ?? "").Trim().ToLowerInvariant();
            if (result != "white" && result != "black" && result != "draw")
            {
                // Mặc định coi là hòa nếu gửi linh tinh
                result = "draw";
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    // 1. Lấy Elo hiện tại của 2 user
                    int whiteEloBefore = 1200;
                    int blackEloBefore = 1200;

                    using (var cmd = new SQLiteCommand(
                        "SELECT UserID, Elo FROM Users WHERE UserID = @w OR UserID = @b",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@w", whiteUserId);
                        cmd.Parameters.AddWithValue("@b", blackUserId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int uid = reader.GetInt32(0);
                                int elo = reader.GetInt32(1);
                                if (uid == whiteUserId) whiteEloBefore = elo;
                                if (uid == blackUserId) blackEloBefore = elo;
                            }
                        }
                    }

                    // 2. Tính Elo sau trận + cờ kết quả
                    int whiteEloAfter = whiteEloBefore;
                    int blackEloAfter = blackEloBefore;

                    bool whiteWin = false;
                    bool blackWin = false;
                    bool isDraw = false;
                    int resultCode;

                    if (result == "white")
                    {
                        whiteEloAfter = whiteEloBefore + 20;
                        blackEloAfter = blackEloBefore - 20;
                        whiteWin = true;
                        resultCode = 0; // trắng thắng
                    }
                    else if (result == "black")
                    {
                        whiteEloAfter = whiteEloBefore - 20;
                        blackEloAfter = blackEloBefore + 20;
                        blackWin = true;
                        resultCode = 1; // đen thắng
                    }
                    else
                    {
                        // hòa
                        isDraw = true;
                        resultCode = 2; // hòa
                    }

                    // 3. Cập nhật Elo trong bảng Users
                    using (var cmd = new SQLiteCommand(
                        "UPDATE Users SET Elo = @elo WHERE UserID = @uid",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@elo", whiteEloAfter);
                        cmd.Parameters.AddWithValue("@uid", whiteUserId);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@elo", blackEloAfter);
                        cmd.Parameters.AddWithValue("@uid", blackUserId);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Cập nhật UserStats (GamesPlayed/Wins/Draws/Losses/BestElo/LastActive)
                    DateTime endTime = DateTime.Now;
                    UpdateUserStatsInternal(conn, tx, whiteUserId, whiteEloAfter, whiteWin, isDraw, endTime);
                    UpdateUserStatsInternal(conn, tx, blackUserId, blackEloAfter, blackWin, isDraw, endTime);

                    // 5. Lưu vào bảng Matches
                    DateTime startTime = endTime; // hiện chưa lưu giờ bắt đầu riêng -> tạm bằng thời điểm kết thúc
                    string startStr = startTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string endStr = endTime.ToString("yyyy-MM-dd HH:mm:ss");

                    using (var cmd = new SQLiteCommand(
                        "INSERT INTO Matches (" +
                        "WhiteUserID, BlackUserID, Result, StartTime, EndTime, " +
                        "TimeControlMinutes, IncrementSeconds, " +
                        "WhiteEloBefore, WhiteEloAfter, BlackEloBefore, BlackEloAfter, MovesPGN, Reason) " +
                        "VALUES (@wId, @bId, @res, @start, @end, @tc, @inc, @wBefore, @wAfter, @bBefore, @bAfter, @pgn, @reason)",
                        conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@wId", whiteUserId);
                        cmd.Parameters.AddWithValue("@bId", blackUserId);
                        cmd.Parameters.AddWithValue("@res", resultCode);
                        cmd.Parameters.AddWithValue("@start", startStr);
                        cmd.Parameters.AddWithValue("@end", endStr);
                        cmd.Parameters.AddWithValue("@tc", timeControlMinutes);
                        cmd.Parameters.AddWithValue("@inc", incrementSeconds);
                        cmd.Parameters.AddWithValue("@wBefore", whiteEloBefore);
                        cmd.Parameters.AddWithValue("@wAfter", whiteEloAfter);
                        cmd.Parameters.AddWithValue("@bBefore", blackEloBefore);
                        cmd.Parameters.AddWithValue("@bAfter", blackEloAfter);
                        cmd.Parameters.AddWithValue("@pgn", DBNull.Value);

                        if (string.IsNullOrEmpty(reason))
                            cmd.Parameters.AddWithValue("@reason", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@reason", reason);

                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
        }
    }
}
