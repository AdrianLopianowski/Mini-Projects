using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace survey
{
    internal class DatabaseConnection
    {
        public static SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        public static string DATABASE_FILE_PATH = "SurveyResponses.db";
        public static string DATABASE_CONNECTION_STRING = "Data Source=" + DATABASE_FILE_PATH + ";Version=3;New=False;Compress=True;";

        public SQLiteCommand Connect()
        {
            try
            {
                sql_con = new SQLiteConnection(DATABASE_CONNECTION_STRING);
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Cannot connect to the database: " + ex.Message);
            }
            return sql_cmd;
        }

        public void Disconnect()
        {
            try
            {
                sql_con.Close();
            }
            catch
            {
                MessageBox.Show("Cannot disconnect from the database");
            }
        }

        public void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(DATABASE_CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Person (
                            PersonId INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirstName TEXT NOT NULL,
                            LastName TEXT NOT NULL
                        )";
                        command.ExecuteNonQuery();

                        command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Survey (
                            SurveyId INTEGER PRIMARY KEY AUTOINCREMENT,
                            PersonId INTEGER,
                            SurveyNumber INTEGER NOT NULL,
                            Answer1 TEXT,
                            Answer2 TEXT,
                            FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
                        )";
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Database successfully created or already exists.");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error creating database: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
