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
                SQLiteConnection.CreateFile("Users.db");
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Email TEXT NOT NULL UNIQUE,
                    DisplayName TEXT NOT NULL,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    Elo INTEGER DEFAULT 1200
                )";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
