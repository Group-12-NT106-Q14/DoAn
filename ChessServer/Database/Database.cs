using System;
using System.Data.SQLite;
using System.IO;

namespace ChessServer
{
    public class Database
    {
        private static string connectionString = "Data Source=Users.db;Version=3;";

        public static void Initialize()
        {
            if (!File.Exists("Users.db"))
            {
                SQLiteConnection.CreateFile("Users.db");
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Bảng Users
                string sqlUsers =
                    "CREATE TABLE IF NOT EXISTS Users (" +
                    "UserID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email TEXT NOT NULL UNIQUE," +
                    "DisplayName TEXT NOT NULL," +
                    "Username TEXT NOT NULL UNIQUE," +
                    "Password TEXT NOT NULL," +
                    "Elo INTEGER DEFAULT 1200" +
                    ")";
                using (var cmd = new SQLiteCommand(sqlUsers, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Bảng thống kê theo user (schema mới, khớp UserRepo + Ranking)
                string sqlStats =
                    "CREATE TABLE IF NOT EXISTS UserStats (" +
                    "UserID INTEGER PRIMARY KEY," +
                    "GamesPlayed INTEGER NOT NULL DEFAULT 0," +
                    "Wins INTEGER NOT NULL DEFAULT 0," +
                    "Draws INTEGER NOT NULL DEFAULT 0," +
                    "Losses INTEGER NOT NULL DEFAULT 0," +
                    "BestElo INTEGER NOT NULL DEFAULT 1200," +
                    "LastActive TEXT," +
                    "FOREIGN KEY(UserID) REFERENCES Users(UserID)" +
                    ")";
                using (var cmd = new SQLiteCommand(sqlStats, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Bảng lịch sử trận đấu (schema mới, khớp SaveMatch + GET_HISTORY)
                string sqlMatches =
                    "CREATE TABLE IF NOT EXISTS Matches (" +
                    "MatchID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "WhiteUserID INTEGER NOT NULL," +
                    "BlackUserID INTEGER NOT NULL," +
                    "Result INTEGER NOT NULL," +              // 0 = trắng thắng, 1 = đen thắng, 2 = hoà
                    "StartTime TEXT," +
                    "EndTime TEXT," +
                    "TimeControlMinutes INTEGER," +
                    "IncrementSeconds INTEGER," +
                    "WhiteEloBefore INTEGER NOT NULL," +
                    "WhiteEloAfter INTEGER NOT NULL," +
                    "BlackEloBefore INTEGER NOT NULL," +
                    "BlackEloAfter INTEGER NOT NULL," +
                    "MovesPGN TEXT," +
                    "Reason TEXT," +
                    "FOREIGN KEY(WhiteUserID) REFERENCES Users(UserID)," +
                    "FOREIGN KEY(BlackUserID) REFERENCES Users(UserID)" +
                    ")";
                using (var cmd = new SQLiteCommand(sqlMatches, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            return conn;
        }
    }
}
